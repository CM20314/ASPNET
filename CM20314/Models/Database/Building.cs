using System;
using System.ComponentModel.DataAnnotations;
namespace CM20314.Models.Database
{
	public class Building : Container
	{
        public string Floors { get; set; }

        public Building()
        {
            
        }
        public Building(string shortName, string longName, string polylineIds, List<int> buildingFloors) : base(shortName, longName, polylineIds)
		{
			Floors = string.Join(',', buildingFloors);
		}
    }
}

