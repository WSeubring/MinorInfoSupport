using Lapiwe.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.Common.Infastructure
{
    public interface IEventPublisher : IDisposable
    {
        void Publish(DomainEvent domainEvent);
    }
}
