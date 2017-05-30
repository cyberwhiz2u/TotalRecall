using NUnit.Framework;

namespace MarsRover.Tests
{
    [TestFixture]
    public class RoverTest
    {
        IPosition _origin = new RoverPosition
        {
            XCoordinate = 0,
            YCoordinate = 0,
            Direction = "N"
        };

        IMap _mapBoundary = new PlateauMap
        {
            XBoundary = 9,
            YBoundary = 9
        };

        [TestCase]
        public void Rover_GivenInvalidInput_ReturnsNull()
        {
            var curiosity = new Rover(_origin);

            var response = curiosity.InputCommand("rter2343432adws", _mapBoundary);

            Assert.IsNull(response);
        }

        [TestCase]
        public void Rover_GivenValidInput_ReturnsPosition()
        {
            IPosition expected = new RoverPosition
            {
                XCoordinate = 0,
                YCoordinate = 1,
                Direction = "N"
            };

            var curiosity = new Rover(_origin);

            var actual = curiosity.InputCommand("WAD", _mapBoundary);

            Assert.AreEqual(expected.XCoordinate, actual.Item1[0,0]);
            Assert.AreEqual(expected.YCoordinate, actual.Item1[0, 1]);
            Assert.AreEqual(expected.Direction, actual.Item2);
        }

        [TestCase]
        public void Rover_GivenValidExecessiveForwardInput_WhichCouldResultInCrossingBoundary_ReturnsLastKnownSafePosition()
        {
            IPosition expected = new RoverPosition
            {
                XCoordinate = 0,
                YCoordinate = 9,
                Direction = "N"
            };

            IPosition roverPosition = new RoverPosition
            {
                XCoordinate = 0,
                YCoordinate = 8,
                Direction = "N"
            };

            var curiosity = new Rover(roverPosition);

            var actual = curiosity.InputCommand("WWWWADDAW", _mapBoundary);

            Assert.AreEqual(expected.XCoordinate, actual.Item1[0, 0]);
            Assert.AreEqual(expected.YCoordinate, actual.Item1[0, 1]);
            Assert.AreEqual(expected.Direction, actual.Item2);
        }

        [TestCase]
        public void Rover_GivenValidExecessiveReverseInput_WhichCouldResultInCrossingBoundary_ReturnsLastKnownSafePosition()
        {
            IPosition expected = new RoverPosition
            {
                XCoordinate = 0,
                YCoordinate = 0,
                Direction = "W"
            };

            IPosition roverPosition = new RoverPosition
            {
                XCoordinate = 2,
                YCoordinate = 0,
                Direction = "W"
            };

            var curiosity = new Rover(roverPosition);

            var actual = curiosity.InputCommand("WWWWWAAAAADDAW", _mapBoundary);

            Assert.AreEqual(expected.XCoordinate, actual.Item1[0, 0]);
            Assert.AreEqual(expected.YCoordinate, actual.Item1[0, 1]);
            Assert.AreEqual(expected.Direction, actual.Item2);
        }
    }
}
