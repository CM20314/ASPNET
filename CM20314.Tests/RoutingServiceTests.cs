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
            Node outputNode = RoutingService.GetNearestNodeToCoordinate(new Coordinate(0.9, 0.9), TestData.Nodes.testNodes.Select(n => new Node(n.Floor, n.BuildingId, n.CoordinateId, coordinate: n.Coordinate, id: n.Id)).ToList());
            Node expectedNode = TestData.Nodes.testNodes.First(n => n.Coordinate.X == 1.0 && n.Coordinate.Y == 1.0);

            Assert.AreEqual(outputNode.Id, expectedNode.Id);
        }

        [TestMethod]
        public void FindNearestNeighbour2()
        {
            Node outputNode = RoutingService.GetNearestNodeToCoordinate(new Coordinate(56, 20), TestData.Nodes.testNodes.Select(n => new Node(n.Floor, n.BuildingId, n.CoordinateId, coordinate: n.Coordinate, id: n.Id)).ToList());
            Node expectedNode = TestData.Nodes.testNodes.First(n => n.Coordinate.X == 100.0 && n.Coordinate.Y == 1.0);

            Assert.AreEqual(outputNode.Id, expectedNode.Id);
        }

        //[TestMethod]
        public void Directions1()
        {
            string outputDirection = RoutingService.GetDirectionStringForNodeArc(TestData.Nodes.testNodeArcs[0], TestData.Nodes.n1_12);
            string expectedDirection = "Bear Right";

            Assert.IsTrue(outputDirection.Equals(expectedDirection));
        }

        [TestMethod]
        public void Directions2()
        {
            string outputDirection = RoutingService.GetDirectionStringForNodeArc(TestData.Nodes.testNodeArcs[0], TestData.Nodes.n2_12);
            string expectedDirection = "Turn Right";

            Assert.IsTrue(outputDirection.Equals(expectedDirection));
        }

        [TestMethod]
        public void Directions3()
        {
            string outputDirection = RoutingService.GetDirectionStringForNodeArc(TestData.Nodes.testNodeArcs[1], TestData.Nodes.n3_12);
            string expectedDirection = "Turn Right";

            Assert.IsTrue(outputDirection.Equals(expectedDirection));
        }

        [TestMethod]
        public void Directions4()
        {
            string outputDirection = RoutingService.GetDirectionStringForNodeArc(TestData.Nodes.testNodeArcs[2], TestData.Nodes.n4_12);
            string expectedDirection = "Turn Right";

            Assert.IsTrue(outputDirection.Equals(expectedDirection));
        }
        //[TestMethod]
        public void Directions5()
        {
            string outputDirection = RoutingService.GetDirectionStringForNodeArc(TestData.Nodes.testNodeArcs[0], TestData.Nodes.n1_13);
            string expectedDirection = "Bear Right";

            Assert.IsTrue(outputDirection.Equals(expectedDirection));
        }

        [TestMethod]
        public void Directions6()
        {
            string outputDirection = RoutingService.GetDirectionStringForNodeArc(TestData.Nodes.testNodeArcs[0], TestData.Nodes.n2_13);
            string expectedDirection = "Turn Left";

            Assert.IsTrue(outputDirection.Equals(expectedDirection));
        }

        [TestMethod]
        public void Directions7()
        {
            string outputDirection = RoutingService.GetDirectionStringForNodeArc(TestData.Nodes.testNodeArcs[1], TestData.Nodes.n3_13);
            string expectedDirection = "Turn Left";

            Assert.IsTrue(outputDirection.Equals(expectedDirection));
        }

        [TestMethod]
        public void Directions8()
        {
            string outputDirection = RoutingService.GetDirectionStringForNodeArc(TestData.Nodes.testNodeArcs[2], TestData.Nodes.n4_13);
            string expectedDirection = "Turn Left";

            Assert.IsTrue(outputDirection.Equals(expectedDirection));
        }
    }
}

