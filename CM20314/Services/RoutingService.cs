using Accord.Collections;
using CM20314.Data;
using CM20314.Models;
using CM20314.Models.Database;
using KdTree;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Numerics;
using System.Runtime.Intrinsics;

namespace CM20314.Services
{
    /// <summary>
    /// Handles routing at a higher level by using the PathfindingService
    /// </summary>
    public class RoutingService
    {
        #pragma warning disable CS8618
        private PathfindingService _pathfindingService;
        private MapDataService _mapDataService;
        private ApplicationDbContext _context;
        private KDTree<Node> kdTree;
        private List<Node> allNodes;
        private List<NodeArc> allNodeArcs;
        #pragma warning restore CS8618

        /// <summary>
        /// Initialise routing service by creating KD tree from map data
        /// </summary>
        /// <param name="pathfindingService">Pathfinding service instance</param>
        /// <param name="mapDataService">Map data service instance</param>
        /// <param name="context">DB context</param>
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

        /// <summary>
        /// Computes a route for a given route request
        /// </summary>
        /// <param name="requestData">Route request data</param>
        /// <returns>Route data</returns>
        public RouteResponseData ComputeRoute(RouteRequestData requestData)
        {
            // Validates request and then calls PathfindingService methods
            Container? endContainer = _mapDataService.SearchContainers(requestData.EndContainerName).FirstOrDefault();
            if(endContainer == null) return new RouteResponseData(new List<NodeArcDirection>(), false, "Cannot find destination", string.Empty);

            if (requestData.StartCoordinate == null)
                return new RouteResponseData(new List<NodeArcDirection>(), false, "No start location specified.", string.Empty);
            Node startNode = GetNearestNodeToCoordinate(requestData.StartCoordinate);

            // Find shortest path
            var nodeArcs = _pathfindingService.FindShortestPath(
                startNode, endContainer, requestData.AccessibilityLevel, allNodes, allNodeArcs);

            if(nodeArcs.Count == 0)
            {
                return new RouteResponseData(new List<NodeArcDirection>(), false, "Unable to find a route.", string.Empty);
            }

            List<NodeArcDirection> arcDirections = new List<NodeArcDirection>();

            // Loop through node arcs (path) and add direction commands
            for(int i = 0; i < nodeArcs.Count; i++)
            {
                NodeArcDirection nodeArcDirection;
                if (i > 0)
                {
                    nodeArcDirection = new NodeArcDirection(nodeArcs.ElementAt(i), GetDirectionStringForNodeArc(nodeArcs.ElementAt(i - 1), nodeArcs.ElementAt(i)));
                }
                else
                {
                    nodeArcDirection = new NodeArcDirection(nodeArcs.ElementAt(i), string.Empty);
                }
                arcDirections.Add(nodeArcDirection);
            }

            return new RouteResponseData(arcDirections, true, string.Empty, endContainer.ShortName);
        }

        /// <summary>
        /// Finds nearest node to user location coordinate
        /// </summary>
        /// <param name="coords">User location</param>
        /// <returns>Nearest node</returns>
        public Node GetNearestNodeToCoordinate(Coordinate coords)
        {
            Node nearestNode = kdTree.Nearest(new double[] { coords.X, coords.Y }).Value;
            return nearestNode;
        }

        /// <summary>
        /// [FOR UNIT TESTING] Finds nearest node to user location coordinate
        /// </summary>
        /// <param name="coords">User location</param>
        /// <returns>Nearest node</returns>
        public static Node GetNearestNodeToCoordinate(Coordinate coords, List<Node> nodes)
        {
            Node[] allNodes = nodes.ToArray();
            var points = nodes.Select(node => new double[] { node.Coordinate.X, node.Coordinate.Y }).ToArray();

            KDTree<Node> kdTree = KDTree.FromData(points, allNodes);
            Node nearestNode = kdTree.Nearest(new double[] { coords.X, coords.Y }).Value;
            return nearestNode;
        }

        /// <summary>
        /// Determines direction string for a node arc based on the angle
        /// </summary>
        /// <param name="arc1">First arc</param>
        /// <param name="arc2">Second (successive) arc</param>
        /// <returns></returns>
        public static string GetDirectionStringForNodeArc(NodeArc arc1, NodeArc arc2)
        {
            if (arc1.Node1Id == arc2.Node1Id)
            {
                SwapNodeArcDirection(arc1);
            }
            else if (arc1.Node2Id == arc2.Node1Id)
            {

            }
            else if (arc1.Node1Id == arc2.Node2Id)
            {
                SwapNodeArcDirection(arc1);
                SwapNodeArcDirection(arc2);
            }
            else // if (arc1.Node2Id == arc2.Node2Id)
            {
                SwapNodeArcDirection(arc2);
            }

            float angle = -1 * AngleBetweenArcs(arc1, arc2);
            System.Diagnostics.Debug.WriteLine($"Angle: {angle}");
            double turningLeftThreshold = - Math.PI / 4 + 0.1;
            double bearingLeftThreshold = - Math.PI / 6 + 0.01;
            double bearingRightThreshold = Math.PI / 6 - 0.01;
            double turningRightThreshold = Math.PI / 4 - 0.1;

            if (angle < turningLeftThreshold)
            {
                return "Turn Right";
            }
            else if (angle < bearingLeftThreshold)
            {
                return arc1.Node2.JunctionSize > 2 ? "Bear Right" : string.Empty;
            }
            else if (angle > turningRightThreshold)
            {
                return "Turn Left";
            }
            else if (angle > bearingRightThreshold)
            {
                return arc1.Node2.JunctionSize > 2 ? "Bear Left" : string.Empty;
            }
            else
            {
                return arc1.Node2.JunctionSize > 4 ? "Go straight" : string.Empty;
            }
        }

        /// <summary>
        /// Swaps the direction of an arc
        /// </summary>
        /// <param name="arc">Arc to swap</param>
        /// <returns>Swapped arc</returns>
        private static NodeArc SwapNodeArcDirection(NodeArc arc)
        {
            Node tempNode = arc.Node1;
            arc.Node1 = arc.Node2;
            arc.Node2 = tempNode;
            arc.Node1Id = arc.Node1.Id;
            arc.Node2Id = arc.Node2.Id;
            return arc;
        }

        /// <summary>
        /// Calculates the angle between two arcs
        /// </summary>
        /// <param name="arc1">Arc 1</param>
        /// <param name="arc2">Arc 2</param>
        /// <returns>Angle (radians)</returns>
        private static float AngleBetweenArcs(NodeArc arc1, NodeArc arc2)
        {
            Vector2 vector1 = new Vector2((float)(arc1.Node2.Coordinate.X - arc1.Node1.Coordinate.X), (float)(arc1.Node2.Coordinate.Y - arc1.Node1.Coordinate.Y));
            Vector2 vector2 = new Vector2((float)(arc2.Node2.Coordinate.X - arc2.Node1.Coordinate.X), (float)(arc2.Node2.Coordinate.Y - arc2.Node1.Coordinate.Y));
            
            // Calculate the dot product
            float dotProduct = Vector2.Dot(vector1, vector2);

            // Calculate the magnitudes of the vectors
            float magnitude1 = vector1.Length();
            float magnitude2 = vector2.Length();

            // Calculate the cosine of the angle
            float cosAngle = dotProduct / (magnitude1 * magnitude2);

            // Calculate the angle in radians
            float angleRad = (float)Math.Acos(cosAngle);

            float crossProduct = vector1.X * vector2.Y - vector2.X * vector1.Y;
            if (crossProduct < 0)
            {
                angleRad = -angleRad; // Adjust the angle sign based on the cross product
            }

            return angleRad;
        }
    }
}
