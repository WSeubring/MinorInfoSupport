using Minor.RoWe.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.RoWe.Common.Interfaces
{
    public interface IEventPublisher : IDisposable
    {
        void Publish(DomainEvent domainEvent);

    }
}
