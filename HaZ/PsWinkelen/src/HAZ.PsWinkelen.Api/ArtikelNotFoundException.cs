using System;

namespace HAZ.PsWinkelen.API.Controllers
{
    internal class ArtikelNotFoundException : Exception
    {
        public ArtikelNotFoundException()
        {
        }

        public ArtikelNotFoundException(string message) : base(message)
        {
        }

        public ArtikelNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}