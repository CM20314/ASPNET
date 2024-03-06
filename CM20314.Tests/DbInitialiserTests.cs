using CM20314.Data;
using CM20314.Models;
using CM20314.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM20314.Tests
{
    [TestClass]
    public class DbInitialiserTests
    {
        public DbInitialiserTests()
        {
            TestData.Coordinates.Initialise();    
        }

        [TestMethod]
        public void CoordinateStandardisationTest1()
        {
            List<Coordinate> coordinates = new List<Coordinate>(TestData.Coordinates.coordinates);
            DbInitialiser.StandardiseCoordinates(coordinates);
            Assert.AreEqual(80, coordinates[0].X);
            Assert.AreEqual(850, coordinates[0].Y);
            Assert.AreEqual(1000, coordinates[1].X);
            Assert.AreEqual(880, coordinates[1].Y);
            Assert.AreEqual(540, coordinates[2].X);
            Assert.AreEqual(400, coordinates[2].Y);
            Assert.AreEqual(0, coordinates[3].X);
            Assert.AreEqual(1000, coordinates[3].Y);
            Assert.AreEqual(40, coordinates[4].X);
            Assert.AreEqual(900, coordinates[4].Y);
        }
        [TestMethod]
        public void NodeArcSplitTest()
        {
            NodeArcSplitSet result = DbInitialiser.SplitNodeArc(TestData.Nodes.longNodeArc1);
            Assert.AreEqual(result.Nodes.Count(), 714);
            Assert.AreEqual(result.NodeArcs.Count(), 715);
        }
    }
}
