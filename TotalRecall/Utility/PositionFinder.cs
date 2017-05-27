using System;

namespace Utility
{
    public class PositionFinder
    {
        private enum CardinalPoints { N = 0, E = 90, S = 180, W = 270 };

        public static Tuple<int[,], string, bool, int[,]> CalculateNewPosition(Tuple<int[,], string> currentPosition, string command, Tuple<int, int> boundary)
        {
            Tuple<int[,], string, bool, int[,]> newPosition = null;

            switch (command)
            {
                case "W": //move forward 1 grid
                case "S": //move back 1 grid
                    newPosition = CalculateNewCoordinates(currentPosition, command, boundary);

                    break;
                case "A": //turn left
                case "D": //turn right
                    var currentCoordinates = new int[,] { { currentPosition.Item1[0,0], currentPosition.Item1[0, 1] } };

                    var currentDirection = currentPosition.Item2;

                    var newDirection = CalculateNewDirection(currentDirection, command);

                    newPosition = new Tuple<int[,], string, bool, int[,]>(currentCoordinates, newDirection, false, null);

                    break;
            }

            return newPosition;
        }

        private static Tuple<int[,], string, bool, int[,]> CalculateNewCoordinates(Tuple<int[,], string> currentPosition, string command, Tuple<int, int> boundary)
        {
            int? value = null;

            int xCoordinate = currentPosition.Item1[0, 0];

            int yCoordinate = currentPosition.Item1[0, 1];

            int[,] currentCoordinates = new int[,] { { xCoordinate, yCoordinate } };

            string currentDirection = currentPosition.Item2;

            int[,] newCoordinates = new int[1, 2];

            int[,] exceededMapBoundaryAtCoordinates = new int[1, 2];

            bool exceededMapBoundary = false;

            switch (currentDirection)
            {
                case "N":
                    if (command == "W")
                    {
                        value = ++yCoordinate;
                    }
                    else if (command == "S")
                    {
                        value = --yCoordinate;
                    }
                    newCoordinates = new int[,] { { xCoordinate, Convert.ToInt32(value) } };
                    break;
                case "E":
                    if (command == "W")
                    {
                        value = ++xCoordinate;
                    }
                    else if (command == "S")
                    {
                        value = --xCoordinate;
                    }
                    newCoordinates = new int[,] { { Convert.ToInt32(value), yCoordinate } };
                    break;
                case "S":
                    if (command == "W")
                    {
                        value = --yCoordinate;
                    }
                    else if (command == "S")
                    {
                        value = ++yCoordinate;
                    }
                    newCoordinates = new int[,] { { xCoordinate, Convert.ToInt32(value) } };
                    break;
                case "W":
                    if (command == "W")
                    {
                        value = --xCoordinate;
                    }
                    else if (command == "S")
                    {
                        value = ++xCoordinate;
                    }
                    newCoordinates = new int[,] { { Convert.ToInt32(value), yCoordinate } };
                    break;
            }

            exceededMapBoundary = HasNewCoordinatesExceededMapBoundary(newCoordinates, boundary);

            if (exceededMapBoundary)
            {
                exceededMapBoundaryAtCoordinates = newCoordinates;
                newCoordinates = currentCoordinates;
            }

            return new Tuple<int[,], string, bool, int[,]>(newCoordinates, currentDirection, exceededMapBoundary, exceededMapBoundaryAtCoordinates);
        }

        private static bool HasNewCoordinatesExceededMapBoundary(int[,] newCoordinates, Tuple<int, int> boundary)
        {
            if ((newCoordinates[0, 0] < 0 || newCoordinates[0, 0] > boundary.Item1) ||
                (newCoordinates[0, 1] < 0 || newCoordinates[0, 1] > boundary.Item2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string CalculateNewDirection(string currentDirection, string command)
        {
            int currentDirectionValue = (int)((CardinalPoints)Enum.Parse(typeof(CardinalPoints), currentDirection));

            int commandValue = (command == "A") ? -90 : 90;

            int newDirectionValue = (currentDirectionValue + commandValue) < 0 ? (360 + (currentDirectionValue + commandValue)) % 360 :
                                                                                    (currentDirectionValue + commandValue) % 360;

            return ((CardinalPoints)newDirectionValue).ToString();
        }
    }
}
