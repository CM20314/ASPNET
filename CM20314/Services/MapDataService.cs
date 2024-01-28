using System;
using CM20314.Data;
using CM20314.Models;
using CM20314.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CM20314.Services
{
	public class MapDataService
    {
        private ApplicationDbContext _context;

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
            return new List<Container>();
        }
    }
}

