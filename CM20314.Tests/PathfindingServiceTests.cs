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
        List<DijkstraNode> nodes;
        List<NodeArc> nodeArcs;
        List<Coordinate> coordinates;

        public PathfindingServiceTests()
        {
            nodes = new List<DijkstraNode>();
            nodeArcs = new List<NodeArc>();
            coordinates = new List<Coordinate>();

            Coordinate c1 = new Coordinate(1.0, 1.0);
            c1.Id = 1;
            DijkstraNode node1 = new DijkstraNode(0, 0, c1.Id, "Node 1");
            node1.Id = 1;

            Coordinate c2 = new Coordinate(2.0, 1.0);
            c2.Id = 2;
            DijkstraNode node2 = new DijkstraNode(0, 0, c2.Id, "Node 2");
            node2.Id = 2;

            Coordinate c3 = new Coordinate(3.0, 1.0);
            c3.Id = 3;
            DijkstraNode node3 = new DijkstraNode(0, 0, c3.Id, "Node 3");
            node3.Id = 3;

            Coordinate c4 = new Coordinate(4.0, 1.0);
            c4.Id = 4;
            DijkstraNode node4 = new DijkstraNode(0, 0, c4.Id, "Node 4");
            node4.Id = 4;

            Coordinate c5 = new Coordinate(5.0, 1.0);
            c5.Id = 5;
            DijkstraNode node5 = new DijkstraNode(0, 0, c5.Id, "Node 5");
            node5.Id = 5;

            Coordinate c6 = new Coordinate(6.0, 1.0);
            c6.Id = 6;
            DijkstraNode node6 = new DijkstraNode(0, 0, c6.Id, "Node 6");
            node6.Id = 6;

            Coordinate c7 = new Coordinate(7.0, 1.0);
            c7.Id = 7;
            DijkstraNode node7 = new DijkstraNode(0, 0, c7.Id, "Node 7");
            node7.Id = 7;

            Coordinate c8 = new Coordinate(8.0, 1.0);
            c8.Id = 8;
            DijkstraNode node8 = new DijkstraNode(0, 0, c8.Id, "Node 8");
            node8.Id = 8;

            Coordinate c9 = new Coordinate(9.0, 1.0);
            c9.Id = 9;
            DijkstraNode node9 = new DijkstraNode(0, 0, c9.Id, "Node 9");
            node9.Id = 9;

            Coordinate c10 = new Coordinate(10.0, 1.0);
            c10.Id = 10;
            DijkstraNode node10 = new DijkstraNode(0, 0, c10.Id, "Node 10");
            node10.Id = 10;

            NodeArc n12 = new NodeArc(node1, node2, true, 4, NodeArcType.Path, false);
            NodeArc n21 = new NodeArc(node2, node1, true, 4, NodeArcType.Path, false);
            NodeArc n13 = new NodeArc(node1, node3, true, 7, NodeArcType.Path, false);
            NodeArc n31 = new NodeArc(node3, node1, true, 7, NodeArcType.Path, false);
            NodeArc n41 = new NodeArc(node4, node1, true, 11, NodeArcType.Path, false);
            NodeArc n14 = new NodeArc(node1, node4, true, 11, NodeArcType.Path, false);

            NodeArc n24 = new NodeArc(node2, node4, true, 7, NodeArcType.Path, false);
            NodeArc n42 = new NodeArc(node4, node2, true, 7, NodeArcType.Path, false);

            NodeArc n34 = new NodeArc(node3, node4, true, 9, NodeArcType.Path, false);
            NodeArc n43 = new NodeArc(node4, node3, true, 9, NodeArcType.Path, false);
            NodeArc n36 = new NodeArc(node3, node6, true, 25, NodeArcType.Path, false);
            NodeArc n63 = new NodeArc(node6, node3, true, 25, NodeArcType.Path, false);
            NodeArc n38 = new NodeArc(node3, node8, true, 20, NodeArcType.Path, false);
            NodeArc n83 = new NodeArc(node8, node3, true, 20, NodeArcType.Path, false);

            NodeArc n45 = new NodeArc(node4, node5, true, 25, NodeArcType.Path, false);
            NodeArc n54 = new NodeArc(node5, node4, true, 25, NodeArcType.Path, false);
            NodeArc n47 = new NodeArc(node4, node7, true, 30, NodeArcType.Path, false);
            NodeArc n74 = new NodeArc(node7, node4, true, 30, NodeArcType.Path, false);

            NodeArc n69 = new NodeArc(node6, node9, true, 23, NodeArcType.Path, false);
            NodeArc n96 = new NodeArc(node9, node6, true, 23, NodeArcType.Path, false);

            NodeArc n710 = new NodeArc(node7, node10, true, 30, NodeArcType.Path, false);
            NodeArc n107 = new NodeArc(node10, node7, true, 30, NodeArcType.Path, false);

            NodeArc n86 = new NodeArc(node8, node6, true, 20, NodeArcType.Path, false);
            NodeArc n68 = new NodeArc(node6, node8, true, 20, NodeArcType.Path, false);

            NodeArc n910 = new NodeArc(node9, node10, true, 50, NodeArcType.Path, false);
            NodeArc n109 = new NodeArc(node10, node9, true, 50, NodeArcType.Path, false);

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

            //Make NodeArcs undirected
            nodeArcs.Add(n41);
            nodeArcs.Add(n31);
            nodeArcs.Add(n21);

            nodeArcs.Add(n24);
            nodeArcs.Add(n42);

            nodeArcs.Add(n34);
            nodeArcs.Add(n36);
            nodeArcs.Add(n38);

            //Make NodeArcs undirected
            nodeArcs.Add(n43);
            nodeArcs.Add(n63);
            nodeArcs.Add(n83);

            nodeArcs.Add(n47);
            nodeArcs.Add(n45);

            //Make NodeArcs undirected
            nodeArcs.Add(n54);
            nodeArcs.Add(n74);

            nodeArcs.Add(n69);
            //Make NodeArc undirected
            nodeArcs.Add(n96);

            nodeArcs.Add(n710);
            //Make NodeArc undirected
            nodeArcs.Add(n107);

            nodeArcs.Add(n86);
            //Make NodeArc undirected
            nodeArcs.Add(n68);

            nodeArcs.Add(n910);
            //Make NodeArc undirected
            nodeArcs.Add(n109);
        }

        [TestMethod]
        public void FindPath1()
        {
            // Test the algorithm with a graph that contains a solution (should work for both undirected/directed)
            System.Diagnostics.Debug.WriteLine("FindPath1: ");
            var pathfindingServiceMock = new Mock<PathfindingService>();

            // List is in reverse order; endNode, node_z, node_y, node_x, startNode
            // Print starting from the end of the list
            List<DijkstraNode> output = pathfindingServiceMock.Object.BreadthFirstSearch(nodes[0], nodes[nodes.Count-1], Models.AccessibilityLevel.None, 
                nodes, nodeArcs);
            List<DijkstraNode> expected = new List<DijkstraNode>() { nodes[nodes.Count-1], nodes[6], nodes[3], nodes[0] };
            
            Assert.AreEqual(expected.Count(), output.Count());
            for(int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(expected[i], output[i]);
            }

            foreach (DijkstraNode node in output)
            {
                System.Diagnostics.Debug.WriteLine(node.getMatchHandle());
            }

        }

        [TestMethod]
        public void FindPath2()
        {
            // Test the algorithm with a graph that contains a solution (shouldn't work for directed, should for undirected)
            System.Diagnostics.Debug.WriteLine("FindPath2: ");
            var pathfindingServiceMock = new Mock<PathfindingService>();

            List<DijkstraNode> output = pathfindingServiceMock.Object.BreadthFirstSearch(nodes[5], nodes[1], Models.AccessibilityLevel.None,
                nodes, nodeArcs);
            List<DijkstraNode> expected = new List<DijkstraNode>() { nodes[1], nodes[0], nodes[2], nodes[5] };

            Assert.AreEqual(expected.Count(), output.Count());
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(expected[i].getMatchHandle(), output[i].getMatchHandle());
            }

            foreach (DijkstraNode node in output)
            {
                System.Diagnostics.Debug.WriteLine(node.getMatchHandle());
            }

        }

        [TestMethod]
        public void FindPath3()
        {
            // Test the algorithm when the start node and end node are the same
            System.Diagnostics.Debug.WriteLine("FindPath3: ");
            var pathfindingServiceMock = new Mock<PathfindingService>();

            List<DijkstraNode> output = pathfindingServiceMock.Object.BreadthFirstSearch(nodes[5], nodes[5], Models.AccessibilityLevel.None,
                nodes, nodeArcs);
            List<DijkstraNode> expected = new List<DijkstraNode>() { nodes[5] };

            Assert.AreEqual(expected.Count(), output.Count());
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(expected[i].getMatchHandle(), output[i].getMatchHandle());
            }

            foreach (DijkstraNode node in output)
            {
                System.Diagnostics.Debug.WriteLine(node.getMatchHandle());
            }
        }

        //[TestMethod]
        public void FindPath4()
        {
            // For this function to pass, make the graph directed
            // Test the algorithm with a graph that doesn't have the solution (test with directed graph; should return an empty list, otherwise it will work with undirected)
            System.Diagnostics.Debug.WriteLine("FindPath4: ");
            var pathfindingServiceMock = new Mock<PathfindingService>();

            List<DijkstraNode> output = pathfindingServiceMock.Object.BreadthFirstSearch(nodes[2], nodes[1], Models.AccessibilityLevel.None,
                nodes, nodeArcs);
            List<DijkstraNode> expected = new List<DijkstraNode>() { };

            Assert.AreEqual(expected.Count(), output.Count());
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(expected[i].getMatchHandle(), output[i].getMatchHandle());
            }

            foreach (DijkstraNode node in output)
            {
                System.Diagnostics.Debug.WriteLine(node.getMatchHandle());
            }

        }

        [TestMethod]
        public void FindPath5()
        {
            // Test the algorithm with a step free accessibility level input
            System.Diagnostics.Debug.WriteLine("FindPath5: ");
            this.nodeArcs[2] = new NodeArc(nodes[0], nodes[3], false, 11, NodeArcType.Path, false);
            var pathfindingServiceMock = new Mock<PathfindingService>();

            List<DijkstraNode> output = pathfindingServiceMock.Object.BreadthFirstSearch(nodes[0], nodes[9], Models.AccessibilityLevel.StepFree,
                nodes, nodeArcs);
            List<DijkstraNode> expected = new List<DijkstraNode>() { nodes[9], nodes[6], nodes[3], nodes[1], nodes[0] };

            Assert.AreEqual(expected.Count(), output.Count());
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(expected[i].getMatchHandle(), output[i].getMatchHandle());
            }

            foreach (DijkstraNode node in output)
            {
                System.Diagnostics.Debug.WriteLine(node.getMatchHandle());
            }
        }
    }
}
