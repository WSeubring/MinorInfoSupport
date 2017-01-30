using Minor.RPCTestProject.Common;
using Minor.WSA.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandReceivingService
{
    public class HyperModerneEventHandler : IEventHandler<HypermodernEvent>
    {
        public void Handle(HypermodernEvent domainEvent)
        {
            Console.WriteLine($"Ik handle deze hypermoderneEvent met het hypermodernegetal {domainEvent.HypermodernGetal}");
        }
    }
}
