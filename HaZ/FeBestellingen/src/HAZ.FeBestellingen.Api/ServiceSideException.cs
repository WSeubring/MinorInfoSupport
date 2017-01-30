using System;

namespace HAZ.FeBestellingen.Api.Controllers
{
    internal class ServiceSideException : Exception
    {
        public ServiceSideException()
        {
        }

        public ServiceSideException(string message) : base(message)
        {
        }

        public ServiceSideException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}