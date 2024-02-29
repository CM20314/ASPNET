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
        private PathfindingService _pathfindingService;
        private MapDataService _mapDataService;
        private ApplicationDbContext _context;
        private KDTree<Node> kdTree;
        private List<Node> allNodes;
        private List<NodeArc> allNodeArcs;

        public void Initialise(
            PathfindingService pathfindingService,
            MapDataService mapDataService,
            ApplicationDbContext context)
        {
            // Acquire services via dependency injection
            _pathfindingService = pathfindingService;
            _mapDataService = mapDataService;
            _context = context;

            Node[] allNodesArr = _context.Node.ToArray();
            foreach (Node node in allNodesArr)
            {
                node.Coordinate = _context.Coordinate.First(c => c.Id == node.CoordinateId);
            }
            var points = allNodesArr.Select(node => new double[] { node.Coordinate.X, node.Coordinate.Y }).ToArray();

            allNodes = allNodesArr.ToList();

            allNodeArcs = _context.NodeArc.ToList();
            foreach (NodeArc arc in allNodeArcs)
            {
                arc.Node1 = _context.Node.First(n => n.Id == arc.Node1Id);
                arc.Node2 = _context.Node.First(n => n.Id == arc.Node2Id);
            }

            kdTree = KDTree.FromData(points, allNodesArr);
        }

        public RouteResponseData ComputeRoute(RouteRequestData requestData)
        {
            // Validates request and then calls PathfindingService methods
            Container endContainer = _mapDataService.SearchContainers(requestData.EndContainerName).FirstOrDefault();
            if(endContainer == null) return new RouteResponseData(new List<NodeArcDirection>(), false, "Cannot find destination");

            if (requestData.StartCoordinate == null)
                return new RouteResponseData(new List<NodeArcDirection>(), false, "No start location specified.");
            Node startNode = GetNearestNodeToCoordinate(requestData.StartCoordinate);

            var nodeArcs = _pathfindingService.FindShortestPath(
                startNode, endContainer, requestData.AccessibilityLevel, allNodes, allNodeArcs);

            List<NodeArcDirection> arcDirections = new List<NodeArcDirection>();

            for(int i = 0; i < nodeArcs.Count; i++)
            {
                NodeArcDirection nodeArcDirection = new NodeArcDirection(nodeArcs.ElementAt(i), GetDirectionStringForNodeArc(nodeArcs.ElementAt(i)));
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
