using CM20314.Models.Database;

namespace CM20314.Models
{
    public class DijkstraNode : Node
    {
        private double distanceFromStartNode;
        private DijkstraNode? previousNode;
        private Boolean visited;
        public DijkstraNode(int floor, int buildingId, int coordinateId, string matchHandle = "") : base(floor, buildingId, coordinateId, matchHandle)
        {
        }

        public DijkstraNode(Node node) : base(node.Floor, node.BuildingId, node.CoordinateId, node.MatchHandle)
        {
            Id = node.Id;
        }

        public double getDistanceFromStartNode() { return distanceFromStartNode; }
        public void setDistanceFromStartNode(double distance) { this.distanceFromStartNode = distance; }

        public void setPreviousNode(DijkstraNode prevNode) { this.previousNode = prevNode; }
        public DijkstraNode getPreviousNode() { return previousNode; }

        public void setVisited(Boolean visited) { this.visited = visited; }
        public Boolean getVisited() { return visited; }
    }
}
