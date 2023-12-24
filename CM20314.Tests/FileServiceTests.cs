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
        [TestMethod]
        public void GetPath()
        {
            var hostingEnvironmentMock = new Mock<IWebHostEnvironment>();
            hostingEnvironmentMock.Setup(m => m.ContentRootPath).Returns($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\source\\repos\\ASPNET\\CM20314");

            var fileServiceMock = new Mock<FileService>(hostingEnvironmentMock.Object);

            var input = "TESTPATH\\TESTFILE";
            string expected = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\source\\repos\\ASPNET\\CM20314\\Data\\Raw\\{input}.txt";

            string output = fileServiceMock.Object.GetPath(input);
            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void ReadLinesFromFile()
        {
            var hostingEnvironmentMock = new Mock<IWebHostEnvironment>();
            var fileServiceMock = new Mock<FileService>(hostingEnvironmentMock.Object);

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
            var hostingEnvironmentMock = new Mock<IWebHostEnvironment>();
            var fileServiceMock = new Mock<FileService>(hostingEnvironmentMock.Object);

            bool result1 = fileServiceMock.Object.FolderExistsForFloor("1W", 2);
            bool result2 = fileServiceMock.Object.FolderExistsForFloor("Non-existent", 0);
            bool result3 = fileServiceMock.Object.FolderExistsForFloor("1WN", 2);

            Assert.IsTrue(result1);
            Assert.IsFalse(result2);
            Assert.IsFalse(result3);
        }
    }
}
