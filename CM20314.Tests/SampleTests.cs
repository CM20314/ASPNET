using CM20314.Services;

namespace CM20314.Tests
{
    [TestClass]
    public class SampleTests
    {
        [TestMethod]
        public void SampleTest1()
        {
            string input = "This is a test string";
            string expected_output = "THIS IS A TEST STRING";
            Assert.AreEqual(expected_output, PathfindingService.DummyMethodToUppercase(input));
        }
    }
}