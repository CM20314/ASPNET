namespace CM20314.Models.Database
{
    public class DijkstraNode : Node
    {
        private double distanceFromStartNode;
        private DijkstraNode? previousNode;
        private Boolean visited;
        public DijkstraNode(int floor, int buildingId, int coordinateId, string matchHandle = "") : base(floor, buildingId, coordinateId, matchHandle)
        {
            Floor = floor;
            BuildingId = buildingId;
            CoordinateId = coordinateId;
            MatchHandle = matchHandle;
        }

        public double getDistanceFromStartNode() { return distanceFromStartNode; }
        public void setDistanceFromStartNode(double distance) { this.distanceFromStartNode = distance; }

        public void setPreviousNode(DijkstraNode prevNode) { this.previousNode = prevNode; }
        public DijkstraNode getPreviousNode() { return previousNode; }

        public void setVisited(Boolean visited) { this.visited = visited; }
        public Boolean getVisited() { return visited; }
    }
}
