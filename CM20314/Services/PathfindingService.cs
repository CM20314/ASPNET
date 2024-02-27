using CM20314.Data;
using CM20314.Models;
using CM20314.Models.Database;

namespace CM20314.Services
{
    // Handles path-finding and direction services
    public class PathfindingService
    {
            public List<NodeArc> FindShortestPath(Node startNode, Container endContainer, AccessibilityLevel accessLevel, List<Node> allNodes, List<NodeArc> allNodeArcs)
        {
            Node targetNode = RoutingService.GetNearestNodeToCoordinate(startNode.Coordinate, allNodes.Where(n => n.BuildingId == endContainer.Id).ToList());

            return AStarSearch(
                startNode, targetNode, accessLevel, allNodes, allNodeArcs).ToList();
        }

        // Uses Dijkstra's Algorithm to perform path search
        public static List<NodeArc> AStarSearch(Node startNode, Node goalNode, AccessibilityLevel accessLevel, List<Node> nodes, List<NodeArc> arcs)
        {
            //return arcs.ToList();
            // Initialize open and closed sets
            var openSet = new List<Node> { startNode };
            var closedSet = new List<Node>();

            // Initialize dictionaries for tracking g, h, f values, and parents
            var gValues = new Dictionary<Node, double>();
            var hValues = new Dictionary<Node, double>();
            var fValues = new Dictionary<Node, double>();
            var parents = new Dictionary<Node, Node>();

            // Initialize start node values
            gValues[startNode] = 0;
            hValues[startNode] = Coordinate.CalculateEucilidianDistance(startNode.Coordinate, goalNode.Coordinate);
            fValues[startNode] = gValues[startNode] + hValues[startNode];

            if(accessLevel == AccessibilityLevel.StepFree)
            {
                arcs = arcs.Where(a => a.StepFree).ToList();
            }

            while (openSet.Count > 0)
            {
                // Find the node with the least f on the open list, call it "q"
                var currentNode = GetLowestFValueNode(openSet, fValues);

                // Pop q off the open list
                openSet.Remove(currentNode);

                // Check if the goal is reached
                if (currentNode == goalNode)
                {
                    // Reconstruct and return the path
                    return ReconstructPath(startNode, goalNode, parents, arcs);
                }

                // Generate q's successors and set their parents to q
                foreach (var arc in arcs.FindAll(a => a.Node1 == currentNode || a.Node2 == currentNode ))
                {
                    var neighbor = (arc.Node1.Id == currentNode.Id) ? arc.Node2 : arc.Node1;

                    // Skip if neighbor is in the closed set
                    if (closedSet.Contains(neighbor))
                        continue;

                    // Compute g, h, f for successor
                    var tentativeGValue = gValues[currentNode] + arc.Cost;

                    if (!openSet.Contains(neighbor) || tentativeGValue < gValues[neighbor])
                    {
                        // Update values and set parent
                        gValues[neighbor] = tentativeGValue;
                        hValues[neighbor] = Coordinate.CalculateEucilidianDistance(neighbor.Coordinate, goalNode.Coordinate);
                        fValues[neighbor] = gValues[neighbor] + hValues[neighbor];
                        parents[neighbor] = currentNode;

                        // Add neighbor to the open list if not already present
                        if (!openSet.Contains(neighbor))
                            openSet.Add(neighbor);
                    }
                }

                // Push q on the closed list
                closedSet.Add(currentNode);
            }

            // No path found
            return new List<NodeArc>();
        }

        private static Node GetLowestFValueNode(List<Node> openSet, Dictionary<Node, double> fValues)
        {
            double minFValue = double.MaxValue;
            Node lowestFValueNode = null;

            foreach (var node in openSet)
            {
                if (fValues.TryGetValue(node, out var fValue) && fValue < minFValue)
                {
                    minFValue = fValue;
                    lowestFValueNode = node;
                }
            }

            return lowestFValueNode;
        }

        private static List<NodeArc> ReconstructPath(Node startNode, Node goalNode, Dictionary<Node, Node> parents, List<NodeArc> nodeArcs)
        {
            var path = new List<NodeArc>();
            var currentNode = goalNode;
            var nextNode = parents.ContainsKey(currentNode) ? parents[currentNode] : null;

            while (currentNode != null && nextNode != null)
            {
                var arc1 = nodeArcs.FirstOrDefault(
                    a => a.Node1.Id == currentNode.Id && a.Node2.Id == nextNode.Id); 
                var arc2 = nodeArcs.FirstOrDefault(
                    a => a.Node2.Id == currentNode.Id && a.Node1.Id == nextNode.Id);

                if(arc1 != null && arc2 == null)
                {
                    arc2 = arc1;
                    Node temp = arc2.Node1;
                    arc2.Node1 = arc2.Node2;
                    arc2.Node2 = temp;
                }

                path.Insert(0, arc2);

                currentNode = nextNode;
                nextNode = parents.ContainsKey(currentNode) ? parents[currentNode] : null;
            }

            return path;
        }

    }
}
