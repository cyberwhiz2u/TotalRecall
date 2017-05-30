using System;
using Utility;

namespace MarsRover
{
    public class Rover
    {
        public Rover(IPosition position)
        {
            Position = position;
        }

        IPosition Position { get; set; }

        public RoverOutput InputCommand(string command, IMap plateauBoundary)
        {
            if (!Validator.IsValidRoverInputCommand(command)) return null;

            return Move(Position, command, plateauBoundary);
        }

        private RoverOutput Move(IPosition position, string command, IMap plateauBoundary)
        {
            char[] instructionList = command.ToCharArray();

            Tuple<int, int> boundary = new Tuple<int, int>(plateauBoundary.XBoundary, plateauBoundary.YBoundary);

            Tuple<int[,], string> currentPosition = new Tuple<int[,], string>(new int[,] { { position.XCoordinate, position.YCoordinate } }, position.Direction);

            Tuple<int[,], string, bool, int[,]> output = null;

            foreach (char instruction in instructionList)
            {
                output = PositionFinder.CalculateNewPosition(currentPosition, instruction.ToString(), boundary);

                if (output.Item3) //boundary limits breached?
                {
                    //currentPosition is NOT updated to newPosition
                    break; //do not process any further instructions if boundary limits
                }
                else
                {
                    //update currentPosition to newPosition
                    currentPosition = new Tuple<int[,], string>(new int[,] { { output.Item1[0, 0], output.Item1[0, 1] } }, output.Item2);
                }
            }

            //move rover to new position
            Position.XCoordinate = currentPosition.Item1[0, 0];

            Position.YCoordinate = currentPosition.Item1[0, 1];

            Position.Direction = currentPosition.Item2;

            //return output
            return new RoverOutput
            {
                CurrentCoordinates = new RoverOutput.Coordinates
                {
                    XValue = Position.XCoordinate, //position on the x axis
                    YValue = Position.YCoordinate //position on the y axis
                },
                CurrentDirection = Position.Direction, //current orientation
                ExceededBoundary = output.Item3,  //has the rover exceeded the boundary
                ExceededBoundaryAtCoordinates = new RoverOutput.Coordinates
                {
                    XValue = (output.Item3 == true) ? output.Item4[0, 0] : 0, //default to 0
                    YValue = (output.Item3 == true) ? output.Item4[0, 1] : 0 //default to 0
                }
            };
        }
    }
}