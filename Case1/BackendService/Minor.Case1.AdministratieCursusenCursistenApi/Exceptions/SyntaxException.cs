using System;

namespace Minor.Case1.AdministratieCursusenCursistenApi.Exceptions
{
    public class SyntaxException : Exception
    {
        public SyntaxException()
        {
        }

        public SyntaxException(string message) : base(message)
        {
        }

        public SyntaxException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}