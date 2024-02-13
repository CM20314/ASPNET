using System;
using CM20314.Models.Database;
using CM20314.Services;

namespace CM20314.Tests
{
    [TestClass]
    public class RoutingServiceTests
    {
        public RoutingServiceTests()
        {
            TestData.Nodes.Initialise();
        }

        [TestMethod]
        public void FindNearestNeighbour1()
		{
            Node outputNode = RoutingService.GetNearestNodeToCoordinate(new Coordinate(0.9, 0.9), TestData.Nodes.testDijkstraNodes.Select(n => new Node(n.Floor, n.BuildingId, n.CoordinateId)).ToList());
            Node expectedNode = TestData.Nodes.testDijkstraNodes.First(n => n.Coordinate.X == 1.0 && n.Coordinate.Y == 1.0);

            Assert.AreEqual(outputNode, expectedNode);
        }
	}
}

