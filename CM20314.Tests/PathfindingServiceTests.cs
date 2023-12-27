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

            //Coordinate c1 = 

            //nodes.add(new Node())
        }

        [TestMethod]
        public void FindPath1()
        {
            var pathfindingServiceMock = new Mock<PathfindingService>();

            string output = pathfindingServiceMock.Object.BreadthFirstSearch(input);
            Assert.AreEqual(expected, output);
        }
    }
}
