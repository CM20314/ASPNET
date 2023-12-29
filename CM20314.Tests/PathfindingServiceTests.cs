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
            Node node1 = new Node(0, 0, c1.Id, "Node 1");

            Coordinate c2 = new Coordinate(2.0, 1.0);
            c2.Id = 2;
            Node node2 = new Node(0, 0, c2.Id, "Node 2");

            Coordinate c3 = new Coordinate(3.0, 1.0);
            c3.Id = 3;
            Node node3 = new Node(0, 0, c3.Id, "Node 3");

            Coordinate c4 = new Coordinate(4.0, 1.0);
            c4.Id = 4;
            Node node4 = new Node(0, 0, c4.Id, "Node 4");

            Coordinate c5 = new Coordinate(5.0, 1.0);
            c5.Id = 5;
            Node node5 = new Node(0, 0, c5.Id, "Node 5");

            Coordinate c6 = new Coordinate(6.0, 1.0);
            c6.Id = 6;
            Node node6 = new Node(0, 0, c6.Id, "Node 6");

            Coordinate c7 = new Coordinate(7.0, 1.0);
            c7.Id = 7;
            Node node7 = new Node(0, 0, c7.Id, "Node 7");

            Coordinate c8 = new Coordinate(8.0, 1.0);
            c8.Id = 8;
            Node node8 = new Node(0, 0, c8.Id, "Node 8");

            Coordinate c9 = new Coordinate(9.0, 1.0);
            c9.Id = 9;
            Node node9 = new Node(0, 0, c9.Id, "Node 9");
           
            Coordinate c10 = new Coordinate(10.0, 1.0);
            c10.Id = 10;
            Node node10 = new Node(0, 0, c10.Id, "Node 10");

            NodeArc n12 = new NodeArc(node1, node2, true, 4, NodeArcType.Path, false);
            NodeArc n13 = new NodeArc(node1, node3, true, 7, NodeArcType.Path, false);
            NodeArc n14 = new NodeArc(node1, node4, true, 11, NodeArcType.Path, false);


            NodeArc n24 = new NodeArc(node2, node4, true, 7, NodeArcType.Path, false);

            NodeArc n34 = new NodeArc(node3, node4, true, 9, NodeArcType.Path, false);
            NodeArc n36 = new NodeArc(node3, node6, true, 25, NodeArcType.Path, false);
            NodeArc n38 = new NodeArc(node3, node8, true, 20, NodeArcType.Path, false);

            NodeArc n45 = new NodeArc(node4, node5, true, 25, NodeArcType.Path, false);
            NodeArc n47 = new NodeArc(node4, node7, true, 30, NodeArcType.Path, false);

            NodeArc n69 = new NodeArc(node6, node9, true, 23, NodeArcType.Path, false);

            NodeArc n710 = new NodeArc(node7, node10, true, 30, NodeArcType.Path, false);

            NodeArc n86 = new NodeArc(node8, node6, true, 20, NodeArcType.Path, false);

            NodeArc n910 = new NodeArc(node9, node10, true, 50, NodeArcType.Path, false);



            nodes.Add(node1);
            nodes.Add(node2);
            nodes.Add(node3);
            nodes.Add(node4);
            nodes.Add(node5);
            nodes.Add(node6);
            nodes.Add(node7);
            nodes.Add(node8);
            nodes.Add(node9);
            nodes.Add(node10);

            coordinates.Add(c1);
            coordinates.Add(c2);
            coordinates.Add(c3);
            coordinates.Add(c4);
            coordinates.Add(c5);
            coordinates.Add(c6);
            coordinates.Add(c7);
            coordinates.Add(c8);
            coordinates.Add(c9);
            coordinates.Add(c10);

            nodeArcs.Add(n12);
            nodeArcs.Add(n13);
            nodeArcs.Add(n14);

            nodeArcs.Add(n24);

            nodeArcs.Add(n34);
            nodeArcs.Add(n36);
            nodeArcs.Add(n38);

            nodeArcs.Add(n45);
            nodeArcs.Add(n47);

            nodeArcs.Add(n69);

            nodeArcs.Add(n710);

            nodeArcs.Add(n86);

            nodeArcs.Add(n910);
        }

        [TestMethod]
        public void FindPath1()
        {
            var pathfindingServiceMock = new Mock<PathfindingService>();

            List<Node> output = pathfindingServiceMock.Object.BreadthFirstSearch(nodes[0], nodes[nodes.Count-1], Models.AccessibilityLevel.None, 
                nodes, nodeArcs);
            List<Node> expected = new List<Node>() { nodes[nodes.Count-1], nodes[6], nodes[3], nodes[0] };
            
            Assert.AreEqual(expected.Count(), output.Count());
            for(int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(expected[i], output[i]);
            }

            foreach (Node node in output)
            {
                System.Diagnostics.Debug.WriteLine(node.getMatchHandle() + "->");
            }

        }
    }
}
