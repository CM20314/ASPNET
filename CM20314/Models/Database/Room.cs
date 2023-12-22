using System;
namespace CM20314.Models.Database
{
	public class Room : Container
	{
		private int primaryFloor;
		private int secondaryFloor;
		private int buildingID;

		public Room(string containerName, Polyline containerPolyline, int primary, int secondary, Building building) : base(containerName, containerPolyline)
		{
			primaryFloor   = primary;
			secondaryFloor = secondary;
			buildingID     = building.Id;
		}

		public int getBuildingID() { return buildingID; }

		//public Building getBuilding(int buildingID) { return ?; }

	}
}

