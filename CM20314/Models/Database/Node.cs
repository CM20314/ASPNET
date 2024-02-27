using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using KdTree;

namespace CM20314.Models.Database
{
	public class Node : Entity
	{
        public int Floor { get; set; }
        public int CoordinateId { get; set; }
        public int BuildingId { get; set; }
        [NotMapped]
        public string MatchHandle { get; set; }
        [NotMapped]
        public Coordinate Coordinate { get; set; }

        public Node()
        {
            
        }
        public Node(int floor, int buildingId, int coordinateId, string matchHandle = "", Coordinate coordinate = null, int id = 0)
		{
		    Floor        = floor;
            BuildingId   = buildingId;
			CoordinateId = coordinateId;
            MatchHandle = matchHandle;
            if (coordinate != null) Coordinate = coordinate;
            if (id != 0) Id = id;
		}
    }
}

