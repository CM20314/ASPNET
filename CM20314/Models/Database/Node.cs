using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
namespace CM20314.Models.Database
{
	public class Node : Entity
	{
		private int floor;
		private int coordinateId;
		private int buildingId;
        private string matchHandle;
        private double distanceFromStartNode;
        private Node previousNode;
        private Boolean visited;

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

        public double getDistanceFromStartNode() {  return distanceFromStartNode; }
        public void setDistanceFromStartNode(double distance) { this.distanceFromStartNode = distance;  }

        public void setPreviousNode(Node prevNode) { this.previousNode = prevNode; }
        public Node getPreviousNode() { return previousNode; }

        public void setVisited(Boolean visited) { this.visited = visited; }
        public Boolean getVisited() {  return visited; }
    }
}

