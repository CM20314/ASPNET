using CM20314.Models;
using CM20314.Models.Database;

namespace CM20314.Services
{
    public class RoutingService
    {
        private readonly PathfindingService _pathfindingService;
        public RoutingService(
            PathfindingService pathfindingService)
        {
            _pathfindingService = pathfindingService;
        }
        public RouteResponseData ComputeRoute(RouteRequestData requestData)
        {
            if(requestData.StartNode == null)
            {
                if (requestData.StartCoordinate == null)
                    return new RouteResponseData(new List<NodeArcDirection>(), false, "No start location specified.");
                requestData.StartNode = GetNearestNodeToCoordinate(requestData.StartCoordinate);
            }

            var nodes = _pathfindingService.FindShortestPath(
                requestData.StartNode, requestData.EndContainer, requestData.AccessibilityLevel);

            List<NodeArcDirection> arcDirections = new List<NodeArcDirection>();

            for(int i = 0; i < nodes.Count - 1; i++)
            {
                NodeArc arc = new NodeArc(nodes.ElementAt(i), nodes.ElementAt(i + 1), false, 0, NodeArcType.Path, false);
                NodeArcDirection nodeArcDirection = new NodeArcDirection(arc, GetDirectionStringForNodeArc(arc));
                arcDirections.Add(nodeArcDirection);
            }

            return new RouteResponseData(arcDirections, true, string.Empty);
        }

        public Node GetNearestNodeToCoordinate(Coordinate coords)
        {
            return new Node(0, 0,0);
        }

        public string GetDirectionStringForNodeArc(NodeArc arc)
        {
            return string.Empty;
        }
    }
}
