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
        public PathfindingServiceTests()
        {
            TestData.Nodes.Initialise();
        }

        [TestMethod]
        public void FindPath1()
        {
            // Test the algorithm with a graph that contains a solution (should work for both undirected/directed)
            System.Diagnostics.Debug.WriteLine("FindPath1: ");
            var pathfindingServiceMock = new Mock<PathfindingService>();

            // List is in reverse order; endNode, node_z, node_y, node_x, startNode
            // Print starting from the end of the list
            List<DijkstraNode> output = pathfindingServiceMock.Object.BreadthFirstSearch(TestData.Nodes.testNodes[0], TestData.Nodes.testNodes[9], Models.AccessibilityLevel.None, 
                TestData.Nodes.testNodes, TestData.Nodes.testNodeArcs);
            List<DijkstraNode> expected = new List<DijkstraNode>() { TestData.Nodes.testNodes[9], TestData.Nodes.testNodes[6], TestData.Nodes.testNodes[3], TestData.Nodes.testNodes[0] };
            
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

            List<DijkstraNode> output = pathfindingServiceMock.Object.BreadthFirstSearch(TestData.Nodes.testNodes[5], TestData.Nodes.testNodes[1], Models.AccessibilityLevel.None,
                TestData.Nodes.testNodes, TestData.Nodes.testNodeArcs);
            List<DijkstraNode> expected = new List<DijkstraNode>() { TestData.Nodes.testNodes[1], TestData.Nodes.testNodes[0], TestData.Nodes.testNodes[2], TestData.Nodes.testNodes[5] };

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

            List<DijkstraNode> output = pathfindingServiceMock.Object.BreadthFirstSearch(TestData.Nodes.testNodes[5], TestData.Nodes.testNodes[5], Models.AccessibilityLevel.None,
                TestData.Nodes.testNodes, TestData.Nodes.testNodeArcs);
            List<DijkstraNode> expected = new List<DijkstraNode>() { TestData.Nodes.testNodes[5] };

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
        public void FindPath4()
        {
            // For this function to pass, make the graph directed
            // Test the algorithm with a graph that doesn't have the solution (test with directed graph; should return an empty list, otherwise it will work with undirected)
            System.Diagnostics.Debug.WriteLine("FindPath4: ");
            var pathfindingServiceMock = new Mock<PathfindingService>();

            List<DijkstraNode> output = pathfindingServiceMock.Object.BreadthFirstSearch(TestData.Nodes.testNodes[1], TestData.Nodes.testNodes[10], Models.AccessibilityLevel.None,
                TestData.Nodes.testNodes, TestData.Nodes.testNodeArcs);
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
            TestData.Nodes.testNodeArcs[2] = new NodeArc(TestData.Nodes.testNodes[0], TestData.Nodes.testNodes[3], false, 11, NodeArcType.Path, false);
            var pathfindingServiceMock = new Mock<PathfindingService>();

            List<DijkstraNode> output = pathfindingServiceMock.Object.BreadthFirstSearch(TestData.Nodes.testNodes[0], TestData.Nodes.testNodes[9], Models.AccessibilityLevel.StepFree,
                TestData.Nodes.testNodes, TestData.Nodes.testNodeArcs);
            List<DijkstraNode> expected = new List<DijkstraNode>() { TestData.Nodes.testNodes[9], TestData.Nodes.testNodes[6], TestData.Nodes.testNodes[3], TestData.Nodes.testNodes[1], TestData.Nodes.testNodes[0] };

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
