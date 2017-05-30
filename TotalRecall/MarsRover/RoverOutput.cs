namespace MarsRover
{
    public class RoverOutput
    {
        public struct Coordinates
        {
            public int XValue { get; set; }

            public int YValue { get; set; }
        }

        public Coordinates CurrentCoordinates { get; set; }

        public string CurrentDirection { get; set; }

        public bool ExceededBoundary { get; set; }

        public Coordinates ExceededBoundaryAtCoordinates { get; set; }
    }
}
