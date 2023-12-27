using CM20314.Data;
using CM20314.Models;
using CM20314.Models.Database;

namespace CM20314.Services
{
    public class PathfindingService
    {
        public List<Node> FindShortestPath(Node startNode, Container endContainer, AccessibilityLevel accessLevel, List<Node> nodes, List<NodeArc> nodeArcs)
        {
            // IMPLEMENT: Call BreadthFirstSearch (change name/params if appropriate) to perform chosen pathfinding algorithm
            //return BreadthFirstSearch(new Node(0, 0, 0),
            //    new Node(0, 0,0), accessLevel);
            return new List<Node>();
        }

        public List<Node> BreadthFirstSearch(Node startNode, Node endNode, AccessibilityLevel accessLevel, List<Node> nodes, List<NodeArc> nodeArcs)
        {
            System.Diagnostics.Debug.WriteLine(startNode.getCoordinateId());
            return new List<Node>();
        }
    }
}
