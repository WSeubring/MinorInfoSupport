using System;

namespace Eventbus
{
    public interface IEventPublisher
    {
        void Publish(DomainEvent domainEvent);
        void Publish(Func<object, object> p);
    }
}