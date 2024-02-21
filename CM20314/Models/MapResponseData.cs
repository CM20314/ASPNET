using CM20314.Models.Database;
using System;
using System.ComponentModel;
namespace CM20314.Models
{
	public class MapResponseData
	{
        public List<Building> Buildings { get; set; }
        public List<Room> Rooms { get; set; }

        public MapResponseData(List<Building> buildings, List<Room> rooms) 
        {
            Buildings = buildings;
            Rooms = rooms;
        }

    }
}

