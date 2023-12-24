using CM20314.Data;
using CM20314.Models;
using CM20314.Models.Database;

namespace CM20314.Services
{
    public class PathfindingService
    {
        private List<Node> allNodes = new List<Node>();
        private List<NodeArc> allNodeArcs = new List<NodeArc>();
        private List<Entity> allEntities = new List<Entity>();

        public void Initialise(IServiceProvider serviceProvider)
        {
            ApplicationDbContext dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            allNodes = dbContext.Node.ToList();
            //allNodeArcs = dbContext.NodeArc.ToList();
            allEntities = dbContext.Entity.ToList();
        }

        public List<Node> FindShortestPath(Node startNode, Container endContainer, AccessibilityLevel accessLevel)
        {
            // IMPLEMENT: Call BreadthFirstSearch (change name/params if appropriate) to perform chosen pathfinding algorithm
            return BreadthFirstSearch(new Node(0, 0, 0),
                new Node(0, 0,0), accessLevel);
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
