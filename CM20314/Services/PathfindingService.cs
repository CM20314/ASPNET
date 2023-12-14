using CM20314.Models;
using CM20314.Models.Database;

namespace CM20314.Services
{
    public class PathfindingService
    {
        private List<Node> allNodes;
        private List<NodeArc> allNodeArcs;

        public PathfindingService(List<Node> mapNodes, List<NodeArc> mapNodeArcs)
        {
            allNodes = mapNodes;
            allNodeArcs = mapNodeArcs;
        }

        public List<Node> FindShortestPath(Node startNode, Node endNode, AccessibilityLevel accessLevel)
        {
            // IMPLEMENT: Call BreadthFirstSearch (change name/params if appropriate) to perform chosen pathfinding algorithm
            return BreadthFirstSearch(startNode, endNode, accessLevel);
        }

        private List<Node> BreadthFirstSearch(Node startNode, Node endNode, AccessibilityLevel accessLevel)
        {
            return new List<Node>();
        }

        // Leave this in to demonstrate how to use unit tests.
        public static string DummyMethodToUppercase(string input)
        {
            return input.ToUpper();
        }
    }
}
