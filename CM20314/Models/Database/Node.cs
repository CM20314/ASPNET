using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace CM20314.Models.Database
{
	public class Node : Entity
	{
		private int floor;
		private int coordinateId;
		private int buildingId;

        public Node()
        {
            
        }
        public Node(int floor, int buildingId, int coordinateId)
		{
			this.floor        = floor;
            this.buildingId   = buildingId;
			this.coordinateId = coordinateId;
		}

		public int getBuildingID() { return buildingId; }

		//public Building getBuilding(int buildingID) { return ?; }

		//public bool isOutside(int coordinateId) { return ?; }

		public int getCoordinateID() { return coordinateId; }

		//public Coordinate getCoordinate(int coordinateID) {return ?;}

		//public Node[] getNeighbouringNodes() { return ?; }

		//public NodeArc[] getNodeArcs(Node[] neighbouringNodes) { return ?; }
	}
}

