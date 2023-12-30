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

        public List<DijkstraNode> BreadthFirstSearch(DijkstraNode startNode, DijkstraNode endNode, AccessibilityLevel accessLevel, List<DijkstraNode> nodes, List<NodeArc> nodeArcs)
        {
            if (startNode.getMatchHandle().Equals(endNode.getMatchHandle())) { return new List<DijkstraNode>() { startNode }; }

            List<DijkstraNode> shortestPath = new List<DijkstraNode>();
            Boolean unvisitedVertexExists = true;

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
                DijkstraNode shortest = null;
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
                    DijkstraNode node1 = nodes.First(n => n.Id == arc.getNode1ID());
                    DijkstraNode node2 = nodes.First(n => n.Id == arc.getNode2ID());


                    if (accessLevel == AccessibilityLevel.StepFree)
                    {
                        if (arc.isStepFree() == false) { continue;  }
                    }

                    if (node1 == shortest)
                    {
                        if (node2.getVisited() == false)
                        {
                            if (arc.getCost() + shortest.getDistanceFromStartNode() < node2.getDistanceFromStartNode())
                            {
                                node2.setDistanceFromStartNode(arc.getCost() + shortest.getDistanceFromStartNode());
                                node2.setPreviousNode(shortest);
                            }
                        }
                    }
                }

                shortest.setVisited(true);

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
