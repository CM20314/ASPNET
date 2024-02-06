using CM20314.Data;
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
        }
    }
}
