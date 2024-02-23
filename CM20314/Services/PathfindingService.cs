using CM20314.Data;
using CM20314.Models;
using CM20314.Models.Database;

namespace CM20314.Services
{
    // Handles path-finding and direction services
    public class PathfindingService
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private ApplicationDbContext _context;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public void Initialise(ApplicationDbContext context)
        {
            _context = context;
        }

            public List<Node> FindShortestPath(Node startNode, Container endContainer, AccessibilityLevel accessLevel, List<Node> allNodes, List<NodeArc> allNodeArcs)
        {
            Node targetNode = RoutingService.GetNearestNodeToCoordinate(startNode.Coordinate, allNodes.Where(n => n.BuildingId == endContainer.Id).ToList());

            return DijkstraPathSearch(
                new DijkstraNode(startNode),
                new DijkstraNode(targetNode),
                accessLevel,
                allNodes.Select(n => new DijkstraNode(n)).ToList(),
                allNodeArcs.ToList()
                ).Select(dn => _context.Node.First(n => n.Id == dn.Id)).ToList();
        }

        // Uses Dijkstra's Algorithm to perform path search
        public List<DijkstraNode> DijkstraPathSearch(DijkstraNode startNode, DijkstraNode endNode, AccessibilityLevel accessLevel, List<DijkstraNode> nodes, List<NodeArc> nodeArcs)
        {
            //if (startNode.MatchHandle.Equals(endNode.MatchHandle)) { return new List<DijkstraNode>() { startNode }; }

            List<DijkstraNode> shortestPath = new List<DijkstraNode>();
            bool unvisitedVertexExists = true;

            foreach (DijkstraNode node in nodes)
            {
                node.setDistanceFromStartNode(int.MaxValue);
            }
            startNode.setDistanceFromStartNode(0);


            foreach (DijkstraNode node in nodes)
            {
                if (node.getVisited() == false)
                {
                    unvisitedVertexExists = true;
                }
            }


            while (unvisitedVertexExists == true)
            {
                DijkstraNode? shortest = null;
                foreach (DijkstraNode node in nodes)
                {
                    if (node.getVisited() == false)
                    {
                        if (shortest == null)
                        {
                            shortest = node;
                        }
                        else if (node.getDistanceFromStartNode() < shortest.getDistanceFromStartNode())
                        {
                            shortest = node;
                        }
                    }
                }



                foreach (NodeArc arc in nodeArcs)
                {
                    DijkstraNode node1 = nodes.First(n => n.Id == arc.Node1Id);
                    DijkstraNode node2 = nodes.First(n => n.Id == arc.Node2Id);


                    if (accessLevel == AccessibilityLevel.StepFree)
                    {
                        if (!arc.StepFree) { continue;  }
                    }

                    if (node1 == shortest)
                    {
                        if (node2.getVisited() == false)
                        {
                            if (arc.Cost + shortest.getDistanceFromStartNode() < node2.getDistanceFromStartNode())
                            {
                                node2.setDistanceFromStartNode(arc.Cost + shortest.getDistanceFromStartNode());
                                node2.setPreviousNode(shortest);
                            }
                        }
                    }
                }

                shortest?.setVisited(true);

                unvisitedVertexExists = false;
                foreach (DijkstraNode node in nodes)
                {
                    if (node.getVisited() == false)
                    {
                        unvisitedVertexExists = true;
                    }
                }


            }

            DijkstraNode end = endNode;
            while (end != startNode && end.getPreviousNode() != null)
            {
                shortestPath.Add(end);
                if (end.getPreviousNode() != null)
                {
                    end = end.getPreviousNode();
                }
            }

            if (end != startNode)
            {
                return new List<DijkstraNode>() { };
            }
            shortestPath.Add(startNode);

            return shortestPath;
        }
    }
}
