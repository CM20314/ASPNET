using System;
namespace CM20314.Models.Database
{
	public class Node : Entity
	{
		private int floor;
		private int coordinateID;
		private int buildingID;

		public Node(int f, Building building, Coordinate coordinate)
		{
			floor        = f;
            buildingID   = building.Id;
            coordinateID = coordinate.Id;
		}

		public int getBuildingID() { return buildingID; }

		//public Building getBuilding(int buildingID) { return ?; }

		//public bool isOutside(int coordinateId) { return ?; }

		public int getCoordinateID() { return coordinateID; }

		//public Coordinate getCoordinate(int coordinateID) {return ?;}

		//public Node[] getNeighbouringNodes() { return ?; }

		//public NodeArc[] getNodeArcs(Node[] neighbouringNodes) { return ?; }
	}
}

