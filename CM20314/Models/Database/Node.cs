using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
namespace CM20314.Models.Database
{
	public class Node : Entity
	{
		protected int floor;
		protected int coordinateId;
		protected int buildingId;
        protected string matchHandle;
        

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

