using Accord.Collections;
using CM20314.Data;
using CM20314.Models;
using CM20314.Models.Database;
using KdTree;

namespace CM20314.Services
{
    // Handles routing at a higher level by using the PathfindingService
    public class RoutingService
    {
        private readonly PathfindingService _pathfindingService;
        private readonly MapDataService _mapDataService;
        private readonly ApplicationDbContext _context;
        private KDTree<Node> kdTree;

        public RoutingService(
            PathfindingService pathfindingService,
            MapDataService mapDataService,
            ApplicationDbContext context)
        {
            // Acquire services via dependency injection
            _pathfindingService = pathfindingService;
            _mapDataService = mapDataService;
            _context = context;

            Node[] allNodes = _context.Node.ToArray();
            foreach(Node node in allNodes)
            {
                node.Coordinate = _context.Coordinate.First(c => c.Id == node.CoordinateId);
            }
            var points = allNodes.Select(node => new double[] { node.Coordinate.X, node.Coordinate.Y }).ToArray();

            kdTree = KDTree.FromData(points, allNodes);

        }
        public RouteResponseData ComputeRoute(RouteRequestData requestData)
        {
            // Validates request and then calls PathfindingService methods
            Container endContainer = _mapDataService.SearchContainers(requestData.EndContainerName).FirstOrDefault();
            if(endContainer == null) return new RouteResponseData(new List<NodeArcDirection>(), false, "Cannot find destination");

            if (requestData.StartCoordinate == null)
                return new RouteResponseData(new List<NodeArcDirection>(), false, "No start location specified.");
            Node startNode = GetNearestNodeToCoordinate(requestData.StartCoordinate);

            var nodes = _pathfindingService.FindShortestPath(
                startNode, endContainer, requestData.AccessibilityLevel, _context.Node.ToList(), _context.NodeArc.ToList());

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
            Node nearestNode = kdTree.Nearest(new double[] { coords.X, coords.Y }).Value;
            return nearestNode;
        }

        // For unit testing
        public static Node GetNearestNodeToCoordinate(Coordinate coords, List<Node> nodes)
        {
            Node[] allNodes = nodes.ToArray();
            var points = nodes.Select(node => new double[] { node.Coordinate.X, node.Coordinate.Y }).ToArray();

            KDTree<Node> kdTree = KDTree.FromData(points, allNodes);
            Node nearestNode = kdTree.Nearest(new double[] { coords.X, coords.Y }).Value;
            return nearestNode;
        }

        public string GetDirectionStringForNodeArc(NodeArc arc)
        {
            // IMPLEMENT
            return string.Empty;
        }
    }
}
