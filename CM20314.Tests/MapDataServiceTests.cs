using CM20314.Models.Database;
using CM20314.Services;
using CM20314.Tests.TestData;
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
                TestData.Containers.rooms.First(r => r.ShortName == "2.59")
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

        [TestMethod]
        public void SearchTest3()
        {
            var mapDataServiceMock = new Mock<MapDataService>();

            string input = "Cha";
            List<Container> output = mapDataServiceMock.Object.SearchContainers(input, TestData.Containers.buildings, TestData.Containers.rooms);
            List<Container> expected = new List<Container>()
            { TestData.Containers.buildings.First(b => b.LongName == "Chancellors' Building"), TestData.Containers.buildings.First(b => b.LongName == "Chaplaincy") };

            foreach (Container container in output)
            {
                System.Diagnostics.Debug.WriteLine(container.LongName);
            }

            Assert.AreEqual(expected.Count(), output.Count());
            Assert.AreEqual(expected.ElementAt(0), output.ElementAt(0));
        }

        [TestMethod]
        public void SearchTest4()
        {
            var mapDataServiceMock = new Mock<MapDataService>();

            string input = "2.5";
            List<Container> output = mapDataServiceMock.Object.SearchContainers(input, TestData.Containers.buildings, TestData.Containers.rooms);
            List<Container> expected = new List<Container>()
            { TestData.Containers.rooms.First(b => b.LongName == "1W 2.59"),
            TestData.Containers.rooms.First(b => b.LongName == "1W 2.57"),
            TestData.Containers.rooms.First(b => b.LongName == "1W 2.56"),
            TestData.Containers.rooms.First(b => b.LongName == "1W 2.53"),
            TestData.Containers.rooms.First(b => b.LongName == "1W 2.58"),
            TestData.Containers.rooms.First(b => b.LongName == "1W 2.54")};

            foreach (Container container in output)
            {
                System.Diagnostics.Debug.WriteLine(container.LongName);
            }

            Assert.AreEqual(expected.Count(), output.Count());
            Assert.AreEqual(expected.ElementAt(0), output.ElementAt(0));
            Assert.AreEqual(expected.ElementAt(1), output.ElementAt(1));
            Assert.AreEqual(expected.ElementAt(2), output.ElementAt(2));
            Assert.AreEqual(expected.ElementAt(3), output.ElementAt(3));
            Assert.AreEqual(expected.ElementAt(4), output.ElementAt(4));
            Assert.AreEqual(expected.ElementAt(5), output.ElementAt(5));
        }

        [TestMethod]
        public void SearchTest5()
        {
            var mapDataServiceMock = new Mock<MapDataService>();

            string input = "SU STV";
            List<Container> output = mapDataServiceMock.Object.SearchContainers(input, TestData.Containers.buildings, TestData.Containers.rooms);
            List<Container> expected = new List<Container>()
            {
            };

            Assert.AreEqual(expected.Count(), output.Count());
        }

        [TestMethod]
        public void SearchTest6()
        {
            var mapDataServiceMock = new Mock<MapDataService>();

            string input = " ";
            List<Container> output = mapDataServiceMock.Object.SearchContainers(input, TestData.Containers.buildings, TestData.Containers.rooms);
            List<Container> expected = new List<Container>()
            {
            };

            Assert.AreEqual(expected.Count(), output.Count());
        }

        [TestMethod]
        public void SearchTest7()
        {
            var mapDataServiceMock = new Mock<MapDataService>();

            string input = "";
            List<Container> output = mapDataServiceMock.Object.SearchContainers(input, TestData.Containers.buildings, TestData.Containers.rooms);
            List<Container> expected = new List<Container>()
            {
            };

            Assert.AreEqual(expected.Count(), output.Count());
        }
    }
}
