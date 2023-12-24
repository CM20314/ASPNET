using CM20314.Services;
using System;
using System.Reflection.Metadata;
namespace CM20314.Models.Database
{
    public class Room : Container
	{
		private int primaryFloor;
		private int secondaryFloor;
		private int buildingId;
		private bool excludeFromRooms;

        public Room()
        {
            
        }

        public Room(string shortName, string longName, string polylineIds, int primaryFloor, int buildingId, bool excludeFromRooms, int secondaryFloor = Constants.SourceFilePaths.FLOOR_OUTDOOR) : base(shortName, longName, polylineIds)
		{
			this.primaryFloor = primaryFloor;
			this.secondaryFloor = secondaryFloor;
			this.buildingId = buildingId;
			this.excludeFromRooms = excludeFromRooms;
		}

        public int getBuildingId() { return buildingId; }
        public int getPrimaryFloor() { return primaryFloor; }
        public int getSecondaryFloor() { return secondaryFloor; }
        public bool shouldExcludeFromRooms() { return excludeFromRooms; }

        //public Building getBuilding(int buildingID) { return ?; }

    }
}

