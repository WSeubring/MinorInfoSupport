using System;

namespace Exeptions
{
    public class RoomnameAlreadyInUseExeption : Exception
    {
            public RoomnameAlreadyInUseExeption() { }
            public RoomnameAlreadyInUseExeption(string message) : base(message) { }
            public RoomnameAlreadyInUseExeption(string message, Exception inner) : base(message, inner) { }
        }
    }
}