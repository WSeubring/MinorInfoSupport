using System;

namespace Kantilever.Magazijnbeheer.Shared
{
    public class ArtikelOnbekendException : Exception
    {
        public ArtikelOnbekendException()
        {
        }

        public ArtikelOnbekendException(string message) : base(message)
        {
        }

        public ArtikelOnbekendException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}