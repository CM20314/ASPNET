using CM20314.Services;
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
    public class FileServiceTests
    {
        private readonly Mock<IWebHostEnvironment> hostingEnvironmentMock;
        private readonly Mock<FileService> fileServiceMock;

        public FileServiceTests()
        {
            hostingEnvironmentMock = new Mock<IWebHostEnvironment>();
            hostingEnvironmentMock.Setup(m => m.ContentRootPath).Returns($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\source\\repos\\ASPNET\\CM20314");

            fileServiceMock = new Mock<FileService>(hostingEnvironmentMock.Object);
        }

        [TestMethod]
        public void GetPath()
        {
            var input = "TESTPATH\\TESTFILE";
            string expected = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\source\\repos\\ASPNET\\CM20314\\Data\\Raw\\{input}.txt";

            string output = fileServiceMock.Object.GetPath(input);
            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void ReadLinesFromFile()
        {
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\source\\repos\\ASPNET\\CM20314\\Data\\Raw\\TestFiles\\Lines.txt";

            List<string> output = fileServiceMock.Object.ReadLinesFromFile(path);
            List<string> expected = new List<string>() { "A", "B", "C", "D" };
            for(int i = 0; i < output.Count; i++)
            {
                Assert.IsTrue(output[i].Equals(expected[i]));
            }
        }

        [TestMethod]
        public void FolderExistsForFloor()
        {
            bool result1 = fileServiceMock.Object.FolderExistsForFloor("1W", 2);
            bool result2 = fileServiceMock.Object.FolderExistsForFloor("Non-existent", 0);
            bool result3 = fileServiceMock.Object.FolderExistsForFloor("1WN", 2);

            Assert.IsTrue(result1);
            Assert.IsFalse(result2);
            Assert.IsFalse(result3);
        }
    }
}
