using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
namespace CM20314.Models.Database
{
	public class Node : Entity
	{
        public int Floor { get; set; }
        public int CoordinateId { get; set; }
        public int BuildingId { get; set; }
        [NotMapped]
        public string MatchHandle { get; set; }      

        public Node()
        {
            
        }
        public Node(int floor, int buildingId, int coordinateId, string matchHandle = "")
		{
		    Floor        = floor;
            BuildingId   = buildingId;
			CoordinateId = coordinateId;
            MatchHandle = matchHandle;
		}

        //public Building getBuilding(int buildingID) { return ?; }

        //public bool isOutside(int coordinateId) { return ?; }

        //public Coordinate getCoordinate(int coordinateID) {return ?;}

        //public Node[] getNeighbouringNodes() { return ?; }

        //public NodeArc[] getNodeArcs(Node[] neighbouringNodes) { return ?; }
    }
}

