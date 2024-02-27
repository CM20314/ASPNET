using CM20314.Data;
using CM20314.Services;
using Moq;
using static CM20314.Helpers.StringExtensions;

namespace CM20314.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void Regular()
        {
            string input = "at point X =    2.0 Y =3.3 Z =  0.0";
            string expected = "at point X =2.0 Y =3.3 Z =0.0";
            Assert.AreEqual(input.FormatCoordinateLine(), expected);
        }
        [TestMethod]
        public void WithNegatives()
        {
            string input = "at point X =    -2.0 Y =3.3 Z =  0.0";
            string expected = "at point X =-2.0 Y =3.3 Z =0.0";
            Assert.AreEqual(input.FormatCoordinateLine(), expected);
        }
    }
}