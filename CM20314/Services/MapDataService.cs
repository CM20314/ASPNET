using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using CM20314.Data;
using CM20314.Models;
using CM20314.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CM20314.Services
{
    // Handles map data search and fetch operations
	public class MapDataService
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private ApplicationDbContext _context;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private static MapResponseData mapResponseData = new MapResponseData(new List<Building>(), new List<Room>());

        public void Initialise(ApplicationDbContext context)
        {
            _context = context;
            // Set on startup, only intialised once across system
            foreach (Building building in _context.Building.ToList())
            {
                List<Coordinate> polylineCoords = new List<Coordinate>();
                foreach (string coordinateId in building.PolylineIds.Split(","))
                {
                    Coordinate coord = _context.Coordinate.First(c => c.Id == Convert.ToInt32(coordinateId));
                    polylineCoords.Add(coord);
                }
                building.Polyline = new Polyline(polylineCoords);
                mapResponseData.Buildings.Add(building);
            }
            foreach (Room room in _context.Room.ToList())
            {
                List<Coordinate> polylineCoords = new List<Coordinate>();
                foreach (string coordinateId in room.PolylineIds.Split(","))
                {
                    Coordinate coord = _context.Coordinate.First(c => c.Id == Convert.ToInt32(coordinateId));
                    polylineCoords.Add(coord);
                }
                room.Polyline = new Polyline(polylineCoords);
                mapResponseData.Rooms.Add(room);
            }
        }

        public MapResponseData GetMapData(int buildingId)
        {
            if(buildingId == 0)
                return mapResponseData;
            MapResponseData filteredResponseData = new MapResponseData(
                mapResponseData.Buildings.Where(b => b.Id == buildingId).ToList(),
                mapResponseData.Rooms.Where(r => r.BuildingId == buildingId).ToList()
                );
            return filteredResponseData;
        }

        public List<Container> SearchContainers(string query, List<Building> buildings, List<Room> rooms)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<Container>();
            }

            query = query.Replace("'", string.Empty);
            query = query.ToUpper();
            List<Container> containers = new List<Container>();

            // For the exact correct input
            // Search through each building
            foreach (Building building in buildings)
            {
                String builLongName = building.LongName.Replace("'", string.Empty);
                builLongName = builLongName.ToUpper();

                // If the input contains the building's long name or short name
                if (query.Contains(builLongName) || query.Contains(building.ShortName.ToUpper()))
                {
                    // Replace the long name with it's short name i.e. 1 West -> 1W
                    query.Replace(builLongName, building.ShortName.ToUpper());

                    // Search each room and check if it's inside the building input
                    foreach (Room room in rooms.Where(r => !r.ExcludeFromRooms))
                    {
                        if (room.BuildingId == building.Id)
                        {
                            // Check if the input matches the room's long name
                            if (query.Contains(room.LongName.ToUpper()))
                            {
                                containers.Add(room);

                                return containers;
                            }
                        }
                    }
                }
            }


            foreach (Building building in buildings)
            {
                String builLongName = building.LongName.Replace("'", string.Empty);
                builLongName = builLongName.ToUpper();
                String builShortName = building.ShortName.Replace("'", string.Empty);
                builShortName = builShortName.ToUpper();

                if (builLongName.Contains(query) || builShortName.Contains(query))
                {
                    containers.Add(building);
                }
            }


            foreach (Room room in rooms.Where(r => !r.ExcludeFromRooms)) {
                String roomLongName = room.LongName.Replace("'", string.Empty);
                roomLongName = roomLongName.ToUpper();
                String roomShortName = room.ShortName.Replace("'", string.Empty);
                roomShortName = roomShortName.ToUpper();

                if (roomShortName.Contains(query) || roomLongName.Contains(query))
                {
                    containers.Add(room);
                }
            }

            foreach (Room room in rooms.Where(r => !r.ExcludeFromRooms))
            {
                if (query.Contains(room.ShortName.ToUpper()) || query.Contains(room.LongName.ToUpper()))
                {
                    containers.Add(room);
                }
            }

            return containers.Take(20).ToList();
        }
    }
}