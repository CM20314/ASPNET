using CM20314.Services;
using CM20314.Models.Database;

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

            // List is in reverse order; endNode, node_z, node_y, node_x, startNode
            // Print starting from the end of the list
            List<NodeArc> output = PathfindingService.AStarSearch(TestData.Nodes.testNodes[0], TestData.Nodes.testNodes[9], new List<Node>() { TestData.Nodes.testNodes[9] }, Models.AccessibilityLevel.None, 
                TestData.Nodes.testNodeArcs);
            List<Node> expectedNodes = new List<Node>() { TestData.Nodes.testNodes[0], TestData.Nodes.testNodes[3], TestData.Nodes.testNodes[6], TestData.Nodes.testNodes[9] };

            Assert.AreEqual(expectedNodes.Count() - 1, output.Count());
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(expectedNodes[i].Id, output[i].Node1Id);
            }
            Assert.AreEqual(expectedNodes.Last().Id, output.Last().Node2Id);
        }

        [TestMethod]
        public void FindPath2()
        {
            // Test the algorithm with a graph that contains a solution (shouldn't work for directed, should for undirected)
            System.Diagnostics.Debug.WriteLine("FindPath2: ");
            
            List<NodeArc> output = PathfindingService.AStarSearch(TestData.Nodes.testNodes[5], TestData.Nodes.testNodes[1], new List<Node>() { TestData.Nodes.testNodes[1] }, Models.AccessibilityLevel.None,
                TestData.Nodes.testNodeArcs);
            List<Node> expectedNodes = new List<Node>() { TestData.Nodes.testNodes[5], TestData.Nodes.testNodes[2], TestData.Nodes.testNodes[0], TestData.Nodes.testNodes[1] };

            Assert.AreEqual(expectedNodes.Count() - 1, output.Count());
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(expectedNodes[i].Id, output[i].Node1Id);
            }
            Assert.AreEqual(expectedNodes.Last().Id, output.Last().Node2Id);

        }

        [TestMethod]
        public void FindPath3()
        {
            // Test the algorithm when the start node and end node are the same
            System.Diagnostics.Debug.WriteLine("FindPath3: ");
            
            List<NodeArc> output = PathfindingService.AStarSearch(TestData.Nodes.testNodes[5], TestData.Nodes.testNodes[5], new List<Node>() { TestData.Nodes.testNodes[5] }, Models.AccessibilityLevel.None,
                TestData.Nodes.testNodeArcs);
            List<Node> expectedNodes = new List<Node>() { TestData.Nodes.testNodes[5] };

            Assert.AreEqual(expectedNodes.Count() - 1, output.Count());
        }

        [TestMethod]
        public void FindPath4()
        {
            // For this function to pass, make the graph directed
            // Test the algorithm with a graph that doesn't have the solution (test with directed graph; should return an empty list, otherwise it will work with undirected)
            System.Diagnostics.Debug.WriteLine("FindPath4: ");
            
            List<NodeArc> output = PathfindingService.AStarSearch(TestData.Nodes.testNodes[1], TestData.Nodes.testNodes[10], new List<Node>() { TestData.Nodes.testNodes[10] }, Models.AccessibilityLevel.None,
                TestData.Nodes.testNodeArcs);
            List<Node> expectedNodes = new List<Node>() { };

            Assert.AreEqual(expectedNodes.Count(), output.Count());
        }

        [TestMethod]
        public void FindPath5()
        {
            // Test the algorithm with a step free accessibility level input
            System.Diagnostics.Debug.WriteLine("FindPath5: ");
            
            TestData.Nodes.testNodeArcs[2].StepFree = false;
            TestData.Nodes.testNodeArcs[3].StepFree = false;
            List<NodeArc> output = PathfindingService.AStarSearch(TestData.Nodes.testNodes[0], TestData.Nodes.testNodes[9], new List<Node>() { TestData.Nodes.testNodes[9] }, Models.AccessibilityLevel.StepFree,
                TestData.Nodes.testNodeArcs);
            List<Node> expectedNodes = new List<Node>() { TestData.Nodes.testNodes[0], TestData.Nodes.testNodes[1], TestData.Nodes.testNodes[3], TestData.Nodes.testNodes[6], TestData.Nodes.testNodes[9] };

            Assert.AreEqual(expectedNodes.Count() - 1, output.Count());
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(expectedNodes[i].Id, output[i].Node1Id);
            }
            Assert.AreEqual(expectedNodes.Last().Id, output.Last().Node2Id);
        }
    }
}
