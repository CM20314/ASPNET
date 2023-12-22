using System;
namespace CM20314.Models.Database
{
	public class Session
	{
        private Coordinate startCoordinate;
        private Coordinate currentCoordinate;
        private Node       startNode;
        private Node       currentNode;
		private Container  endContainer;

		public Session(Coordinate startCoord, Coordinate currentCoord, Node startNo, Node currentNo, Container endCont)
		{
			startCoordinate   = startCoord;
			currentCoordinate = currentCoord;
			startNode         = startNo;
			currentNode       = currentNo;
			endContainer      = endCont;
		}

        public Coordinate getStartCoordinate() { return startCoordinate; }
        public Coordinate getCurrentCoordinate() { return currentCoordinate; }
		public Node getStartNode() { return startNode; }
		public Node getCurrentNode() { return currentNode;}
		public Container getEndContainer() {  return endContainer; }
		public void setCurrentCoordinate(Coordinate coordinate) { this.currentCoordinate = coordinate; }
		public void setCurrentNode(Node node) { this.startNode = node; }
		public void setEndContainer(Container container) { this.endContainer = container; }


	}
}

