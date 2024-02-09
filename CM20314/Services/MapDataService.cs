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
            List<Container> containers = new List<Container>();
            foreach (Building building in buildings)
            {
                if (query.Contains(building.LongName) || query.Contains(building.ShortName))
                {
                    query.Replace(building.LongName, building.ShortName);
                    foreach (Room room in rooms.Where(r => !r.ExcludeFromRooms))
                    {
                        if (room.BuildingId == building.Id)
                        {
                            if (query.Contains(room.LongName))
                            {
                                containers.Add(room);

                                return containers;
                            }
                        }
                    }
                }

                //query = "1W 2.59
            }

            foreach (Room room in rooms.Where(r => !r.ExcludeFromRooms)
            {
                if (query.Contains(room.ShortName) || query.Contains(room.LongName))
                {
                    containers.Add(room);
                }
            }

            
            foreach (Container container in containers)
            {
                System.Diagnostics.Debug.WriteLine(container.LongName + " " + container.ShortName);
            }
            return containers;
        }
    }
}