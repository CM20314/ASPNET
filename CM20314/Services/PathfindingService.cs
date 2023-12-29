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
            List<Node> shortestPath = new List<Node>();
            Boolean unvisitedVertexExists = true;

            foreach (Node node in nodes)
            {
                node.setDistanceFromStartNode(int.MaxValue);
            }
            startNode.setDistanceFromStartNode(0);


            foreach (Node node in nodes)
            {
                if (node.getVisited() == false)
                {
                    unvisitedVertexExists = true;
                }
            }


            while (unvisitedVertexExists == true)
            {
                Node shortest = null;
                foreach (Node node in nodes)
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



                List<Node> neighbours = new List<Node>();
                foreach (NodeArc arc in nodeArcs)
                {
                    Node node1 = nodes.First(n => n.Id == arc.getNode1ID());
                    Node node2 = nodes.First(n => n.Id == arc.getNode2ID());


                    if (accessLevel == AccessibilityLevel.StepFree)
                    {
                        if (arc.isStepFree() == false) { continue;  }
                    }

                    if (arc.getNode1() == shortest)
                    {
                        if (arc.getNode2().getVisited() == false)
                        {
                            if (arc.getCost() + shortest.getDistanceFromStartNode() < arc.getNode2().getDistanceFromStartNode())
                            {
                                arc.getNode2().setDistanceFromStartNode(arc.getCost() + shortest.getDistanceFromStartNode());
                                arc.getNode2().setPreviousNode(shortest);
                            }
                            neighbours.Add(arc.getNode2());
                        }
                    }
                }

                shortest.setVisited(true);

                unvisitedVertexExists = false;
                foreach (Node node in nodes)
                {
                    if (node.getVisited() == false)
                    {
                        unvisitedVertexExists = true;
                    }
                }


            }

            Node end = endNode;
            while (end != startNode)
            {
                shortestPath.Add(end);
                end = end.getPreviousNode();
            }
            shortestPath.Add(startNode);

            return shortestPath;
        }
    }
}
