namespace MarsRover
{
    public interface IPosition
    {
        int XCoordinate { get; set; }

        int YCoordinate { get; set; }

        string Direction { get; set; }
    }
}
