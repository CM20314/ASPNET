using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace CM20314.Models.Database
{
	public class Node : Entity
	{
		private int floor;
		private int coordinateId;
		private int buildingId;
        private string matchHandle;

        public Node()
        {
            
        }
        public Node(int floor, int buildingId, int coordinateId, string matchHandle = "")
		{
			this.floor        = floor;
            this.buildingId   = buildingId;
			this.coordinateId = coordinateId;
            this.matchHandle = matchHandle;
		}

		public int getBuildingId() { return buildingId; }

        //public Building getBuilding(int buildingID) { return ?; }

        //public bool isOutside(int coordinateId) { return ?; }

        public int getCoordinateId() { return coordinateId; }
        public int getFloor() { return floor; }
        public string getMatchHandle() { return matchHandle; }
        public void setMatchHandle(string value) { matchHandle = value; }

        //public Coordinate getCoordinate(int coordinateID) {return ?;}

        //public Node[] getNeighbouringNodes() { return ?; }

        //public NodeArc[] getNodeArcs(Node[] neighbouringNodes) { return ?; }
    }
}

