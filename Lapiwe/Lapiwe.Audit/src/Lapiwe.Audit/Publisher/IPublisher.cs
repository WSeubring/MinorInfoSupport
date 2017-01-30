using Lapiwe.Audit.Domain;
using System;

namespace Lapiwe.Audit.Publisher
{
    public interface IPublisher : IDisposable
    {
        void Publish(string queueName, SerializedEvent message);
    }
}