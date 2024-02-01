using CM20314.Models.Database;
using CM20314.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM20314.Tests
{
    [TestClass]
    public class MapDataServiceTests
    {
        public MapDataServiceTests()
        {
            TestData.Containers.Initialise();
        }

        [TestMethod]
        public void SearchTest1()
        {
            var mapDataServiceMock = new Mock<MapDataService>();

            string input = "1W 2.59";
            List<Container> output = mapDataServiceMock.Object.SearchContainers(input, TestData.Containers.buildings, TestData.Containers.rooms);
            List<Container> expected = new List<Container>()
            {
                TestData.Containers.rooms.First(r => r.getLongName() == "2.59")
            };
            Assert.AreEqual(expected.Count(), output.Count());
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(expected[i], output[i]);
            }
        }

        [TestMethod]
        public void SearchTest2()
        {
            var mapDataServiceMock = new Mock<MapDataService>();

            string input = "1W C2.03";
            List<Container> output = mapDataServiceMock.Object.SearchContainers(input, TestData.Containers.buildings, TestData.Containers.rooms);
            List<Container> expected = new List<Container>()
            {
            };
            Assert.AreEqual(expected.Count(), output.Count());
        }
    }
}
