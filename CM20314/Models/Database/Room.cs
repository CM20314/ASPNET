using CM20314.Services;
using System;
using System.Reflection.Metadata;
namespace CM20314.Models.Database
{
    public class Room : Container
	{
        public int PrimaryFloor { get; set; }
        public int SecondaryFloor { get; set; }
        public int BuildingId { get; set; }
        public bool ExcludeFromRooms { get; set; }

        public Room()
        {
            
        }

        public Room(string shortName, string longName, string polylineIds, int primaryFloor, int buildingId, bool excludeFromRooms, int secondaryFloor = Constants.SourceFilePaths.FLOOR_OUTDOOR) : base(shortName, longName, polylineIds)
		{
			PrimaryFloor = primaryFloor;
			SecondaryFloor = secondaryFloor;
			BuildingId = buildingId;
			ExcludeFromRooms = excludeFromRooms;
		}

        //public Building getBuilding(int buildingID) { return ?; }

    }
}

