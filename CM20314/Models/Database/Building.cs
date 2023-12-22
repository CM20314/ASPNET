using System;
using System.ComponentModel.DataAnnotations;
namespace CM20314.Models.Database
{
	public class Building : Container
	{
		private string floors;

        public Building() : base()
        {
            
        }
        public Building(string shortName, string longName, Polyline containerPolyline, List<int> buildingFloors) : base(shortName, longName, containerPolyline)
		{
			floors = string.Join(',', buildingFloors);
		}

		//public ? getMapForFloor(int floor) { }
    }
}

