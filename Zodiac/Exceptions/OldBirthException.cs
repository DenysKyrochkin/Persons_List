using System;

namespace Zodiac.Exceptions
{
    internal class OldBirthException : ArgumentOutOfRangeException
    {
        public OldBirthException(string message) : base(message)
        {
        }

        public OldBirthException() : base("Too old date")
        {
        }
    }
}
