using System;
using System.ComponentModel.DataAnnotations;
namespace CM20314.Models.Database
{
	public class Building : Container
	{
		private string floors;
		private string polylineIds;

        public Building()
        {
            
        }
        public Building(string shortName, string longName, string polylineIds, List<int> buildingFloors) : base(shortName, longName, polylineIds)
		{
			floors = string.Join(',', buildingFloors);
			this.polylineIds = polylineIds;
		}

		//public ? getMapForFloor(int floor) { }
    }
}

