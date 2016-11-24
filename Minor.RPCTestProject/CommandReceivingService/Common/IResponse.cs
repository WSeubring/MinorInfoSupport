using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandReceivingService.Common
{
    public interface IResponse
    {
         ResponseStatus Status{ get; set; }
    }
}
