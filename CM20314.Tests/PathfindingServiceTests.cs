using CM20314.Services;
using CM20314.Models.Database;
using Microsoft.AspNetCore.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM20314.Tests
{
    [TestClass]
    public class PathfindingServiceTests
    {
        List<Node> nodes;
        List<NodeArc> nodeArcs;
        List<Coordinate> coordinates;

        public PathfindingServiceTests()
        {
            nodes = new List<Node>();
            nodeArcs = new List<NodeArc>();
            coordinates = new List<Coordinate>();

            Coordinate c1 = new Coordinate(1.0, 1.0);
            c1.Id = 1;
            Node node1 = new Node(0, 0, c1.Id);
            Coordinate c2 = new Coordinate(2.0, 1.0);
            c2.Id = 2;
            Node node2 = new Node(0, 0, c2.Id);

            NodeArc na1 = new NodeArc(node1, node2, true, 1, NodeArcType.Path, false);

            nodes.Add(node1);
            nodes.Add(node2);
            coordinates.Add(c1);
            coordinates.Add(c2);
            nodeArcs.Add(na1);
        }

        [TestMethod]
        public void FindPath1()
        {
            var pathfindingServiceMock = new Mock<PathfindingService>();

            List<Node> output = pathfindingServiceMock.Object.BreadthFirstSearch(nodes[0], nodes[1], Models.AccessibilityLevel.None, 
                nodes, nodeArcs);
            List<Node> expected = new List<Node>() { nodes[0], nodes[1] };
            
            Assert.AreEqual(expected.Count(), output.Count());
            for(int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(expected[i], output[i]);
            }
        }
    }
}
