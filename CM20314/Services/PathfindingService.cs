﻿using CM20314.Data;
using CM20314.Models;
using CM20314.Models.Database;

namespace CM20314.Services
{
    public class PathfindingService
    {
        private List<Node> allNodes = new List<Node>();
        private List<NodeArc> allNodeArcs = new List<NodeArc>();

        public void Initialise(IServiceProvider serviceProvider)
        {
            ApplicationDbContext dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            allNodes = dbContext.Node.ToList();
            allNodeArcs = dbContext.NodeArc.ToList();
        }

        public List<Node> FindShortestPath(Node startNode, Container endContainer, AccessibilityLevel accessLevel)
        {
            // IMPLEMENT: Call BreadthFirstSearch (change name/params if appropriate) to perform chosen pathfinding algorithm
            return BreadthFirstSearch(new Node(), new Node(), accessLevel);
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
