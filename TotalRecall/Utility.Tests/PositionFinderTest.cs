using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Utility.Tests
{
    [TestClass]
    public class PositionFinderTest
    {
        Tuple<int, int> _mapBoundary = new Tuple<int, int>(9, 9);

        [TestMethod]
        public void PositionFinder_GivenDirectionIsNorthAndInputCommandIsW_ReturnsYCoordinatePlus1()
        {
            var currentPosition = new Tuple<int[,], string>((new int[,] { { 0, 0 } }), "N");

            var command = "W";

            var expected = new Tuple<int, int, string>(0, 1, "N");

            var response = PositionFinder.CalculateNewPosition(currentPosition, command, _mapBoundary);

            Assert.AreEqual(expected.Item1, response.Item1[0,0]);

            Assert.AreEqual(expected.Item2, response.Item1[0, 1]);

            Assert.AreEqual(expected.Item3, response.Item2);
        }

        [TestMethod]
        public void PositionFinder_GivenDirectionIsSouthAndInputCommandIsA_ReturnsDirectionEast()
        {
            var currentPosition = new Tuple<int[,], string>((new int[,] { { 0, 0 } }), "S");

            var command = "A";

            var expected = new Tuple<int, int, string>(0, 0, "E");

            var response = PositionFinder.CalculateNewPosition(currentPosition, command, _mapBoundary);

            Assert.AreEqual(expected.Item1, response.Item1[0, 0]);

            Assert.AreEqual(expected.Item2, response.Item1[0, 1]);

            Assert.AreEqual(expected.Item3, response.Item2);
        }
    }
}
