namespace Eventbus
{
    public interface IEventPublisher
    {
        void Publish(DomainEvent domainEvent);
    }
}