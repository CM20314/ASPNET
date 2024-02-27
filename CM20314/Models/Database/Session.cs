using System;
namespace CM20314.Models.Database
{
	public class Session
	{
        public Coordinate StartCoordinate { get; set; }
        public Coordinate CurrentCoordinate { get; set; }
        public Node StartNode { get; set; }
        public Node CurrentNode { get; set; }
        public Container EndContainer { get; set; }

		public Session(Coordinate startCoord, Coordinate currentCoord, Node startNo, Node currentNo, Container endCont)
		{
			StartCoordinate   = startCoord;
			CurrentCoordinate = currentCoord;
			StartNode         = startNo;
			CurrentNode       = currentNo;
			EndContainer      = endCont;
		}
	}
}

