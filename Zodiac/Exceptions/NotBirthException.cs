using System;

namespace Zodiac.Exceptions
{
    internal class NotBirthException : ArgumentOutOfRangeException
    {
        public NotBirthException(string message) : base(message)
        {
        }

        public NotBirthException() : base("Date from the future")
        {
        }
    }
}
