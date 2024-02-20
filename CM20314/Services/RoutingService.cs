using CM20314.Data;
using CM20314.Models;
using CM20314.Models.Database;

namespace CM20314.Services
{
    // Handles routing at a higher level by using the PathfindingService
    public class RoutingService
    {
        private readonly PathfindingService _pathfindingService;
        private readonly ApplicationDbContext _context;

        public RoutingService(
            PathfindingService pathfindingService,
            ApplicationDbContext context)
        {
            // Acquire services via dependency injection
            _pathfindingService = pathfindingService;
            _context = context;
        }
        public RouteResponseData ComputeRoute(RouteRequestData requestData)
        {
            // Validates request and then calls PathfindingService methods
            /*if(requestData.StartNode == null)
            {
                if (requestData.StartCoordinate == null)
                    return new RouteResponseData(new List<NodeArcDirection>(), false, "No start location specified.");
                requestData.StartNode = GetNearestNodeToCoordinate(requestData.StartCoordinate);
            }

            var nodes = _pathfindingService.FindShortestPath(
                requestData.StartNode, requestData.EndContainer, requestData.AccessibilityLevel, _context.Node.ToList(), _context.NodeArc.ToList());

            List<NodeArcDirection> arcDirections = new List<NodeArcDirection>();

            for(int i = 0; i < nodes.Count - 1; i++)
            {
                NodeArc arc = new NodeArc(nodes.ElementAt(i), nodes.ElementAt(i + 1), false, 0, NodeArcType.Path, false);
                NodeArcDirection nodeArcDirection = new NodeArcDirection(arc, GetDirectionStringForNodeArc(arc));
                arcDirections.Add(nodeArcDirection);
            }

            return new RouteResponseData(arcDirections, true, string.Empty);*/
            return new RouteResponseData(new List<NodeArcDirection>(), false, "Requires implementation");
        }

        public Node GetNearestNodeToCoordinate(Coordinate coords)
        {
            // IMPLEMENT
            return new Node(0,0,0);
        }

        public string GetDirectionStringForNodeArc(NodeArc arc)
        {
            // IMPLEMENT
            return string.Empty;
        }
    }
}
