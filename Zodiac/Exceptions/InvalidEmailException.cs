using System;

namespace Zodiac.Exceptions
{
    internal class InvalidEmailException : FormatException
    {
        public InvalidEmailException(string message) : base(message)
        {
        }

        public InvalidEmailException() : base("Invalid email.")
        {
        }
    }
}