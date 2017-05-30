using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandLine = "";

            Console.WriteLine("\nTotal Recall - Curiosity Rover - Allowed Commands (Case Senstive): " +
                              "\nW >> Move 1 Position Forward" +
                              "\nA >> Turn Left " +
                              "\nD >> Turn Right" +
                              "\nEND >> to complete the Mission");

            IPosition _roverPosition = new RoverPosition
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

            Rover rover = new Rover(_roverPosition);

            var position = _mapBoundary.XBoundary.ToString() +
                            " by " + _mapBoundary.YBoundary.ToString();

            Console.WriteLine($"\nThe terain of interest is a plateau that is : {position}");
            Console.WriteLine("\nAny instructions pased to Curiosity rover that will result in Curiosity exceeding these boundaries will be ignored. " +
                "Curiosity is smart enough to move to the last known safe position as well as report back the unsafe position you were trying to send her to.");

            position = "(" + _roverPosition.XCoordinate.ToString() +
                            ", " + _roverPosition.YCoordinate.ToString() +
                            ", " + _roverPosition.Direction + ")";

            Console.WriteLine($"\nCuriosity rover is now at: {position}");

            try
            {
                while (commandLine != "END")
                {
                    Console.WriteLine("\nEnter Command: ");
                    commandLine = Console.ReadLine();

                    if (commandLine == "END")
                        break;

                    Console.WriteLine($"\nThe command entered was: {commandLine}");

                    var result = rover.InputCommand(commandLine, _mapBoundary);

                    if (result != null)
                    {
                        position = "(" + result.Item1[0, 0].ToString() +
                                        ", " + result.Item1[0, 1].ToString() +
                                        ", " + result.Item2 + ")";

                        Console.WriteLine($"\nCuriosity rover is now at: {position}");

                        if (result.Item3)
                        {
                            Console.WriteLine("\nTerminated at last safe position. Proceeding as per instruction provided would have caused Curiosity rover to exceed the boundary");
                            var obstaclePosition = "(" + result.Item4[0, 0].ToString() +
                                            ", " + result.Item4[0, 1].ToString() + ")";

                            Console.WriteLine($"\nCuriosity rover would have crossed the boundary at : {obstaclePosition}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nERROR - Invalid command. Try Again.");
                    }

                    commandLine = "";
                }

                Console.WriteLine("\nMission Ended!!");
                Console.ReadLine();
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp);
                Console.ReadLine();
            }
        }
    }
}

