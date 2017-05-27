using System;
using System.Text.RegularExpressions;

namespace Utility
{
    public class Validator
    {
        public static bool IsValidRoverInputCommand(string command)
        {
            //Check for Null or WhiteSpace or Empty string
            if (String.IsNullOrWhiteSpace(command)) return false;

            //Case sensitive check - only valid commands are W, A & D
            Regex Validator = new Regex(@"^[WADS]+$");
            if (!Validator.IsMatch(command)) return false;

            return true;
        }
    }
}
