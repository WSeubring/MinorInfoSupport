using System;

namespace Kantilever.Magazijnbeheer.Shared
{
    public class OnvoldoendeVoorraadException : Exception
    {
        public OnvoldoendeVoorraadException()
        {
        }

        public OnvoldoendeVoorraadException(string message) : base(message)
        {
        }

        public OnvoldoendeVoorraadException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
