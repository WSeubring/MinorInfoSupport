using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaZ.DSBestellingenBeheer.Facade.Errors
{
    public enum ErrorType
    {
        Unknown = 0,
        DuplicateKey = 10,
        NotFound = 20,
        BadRequest = 30,
    }
}
