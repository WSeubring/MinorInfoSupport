using System;
using System.Collections.Generic;

namespace Lapiwe.Common.Domain
{
    public class FunctionalException : Exception
    {
        public List<FunctionalError> FunctionalErrors { get; private set; }

        public FunctionalException()
        {
            FunctionalErrors = new List<FunctionalError>();
        }

        public FunctionalException(FunctionalError error) : this()
        {
            Add(error);
        }

        public void Add(FunctionalError error)
        {
            FunctionalErrors.Add(error);
        }

    }
}
