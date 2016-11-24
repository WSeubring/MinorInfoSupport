using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.Common
{
    public class FunctionalError
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public FunctionalError(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
