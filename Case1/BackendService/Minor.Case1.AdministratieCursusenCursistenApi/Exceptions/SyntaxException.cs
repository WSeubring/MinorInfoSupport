using System;

namespace Minor.Case1.AdministratieCursusenCursistenApi.Exceptions
{
    public class InvalidSyntaxException : Exception
    {
        public InvalidSyntaxException()
        {
        }

        public InvalidSyntaxException(string message) : base(message)
        {
        }

        public InvalidSyntaxException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}