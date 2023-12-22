using System;
using System.ComponentModel.DataAnnotations;
namespace CM20314.Models.Database
{
	public class Building : Container
	{
		private int[] floors;

		public Building(string containerName, Polyline containerPolyline, int[] buildingFloors) : base(containerName, containerPolyline)
		{
			floors = buildingFloors;
		}

		//public ? getMapForFloor(int floor) { }
    }
}

