using Lapiwe.Audit.Domain;
using Lapiwe.Audit.Domain.Contracts;
using Lapiwe.Audit.Listener;
using Lapiwe.Audit.Publisher;
using Lapiwe.Audit.Test.TestObjects;
using Lapiwe.EventBus.Domain;
using Lapiwe.EventBus.Publishers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Minor.WSA.Audit.Test
{

    [TestClass]
    public class AuditIntegrationTest
    {
        private BusOptions _busOptions;
        private BusOptions _direct;

        [TestInitialize]
        public void Init()
        {
            _busOptions = new BusOptions { ExchangeName = "TestExchange1", HostName = "localhost", Port = 5672, Username = "guest", Password = "guest" };
            _direct = new BusOptions { ExchangeName = "", HostName = "localhost", Port = 5672, Username = "guest", Password = "guest" };
        }

        [TestMethod]
        public void PublishEventsToCertainQueueTest()
        {
            var repoMock = new Mock<IRepository>(MockBehavior.Strict);
            repoMock.Setup(repo => repo.FindBy(It.IsAny<Expression<Func<SerializedEvent, bool>>>())).Returns(() => new List<SerializedEvent>() { new SerializedEvent() { ID = 1337, TimeReceived = DateTime.Now, Body = "test body", EventType = typeof(TestEvent).FullName, RoutingKey = "#" } });
            var queueName = "TestQueue1";
            using (var sender = new EventPublisher(_busOptions))
            using (var receiver = new TestDispatcher(_direct, queueName))
            using (var publisher = new AuditPublisher(_direct))
            using (var dispatcher = new AuditDispatcher(repoMock.Object, publisher, _busOptions))
            {
                var saec = new SendAllEventCommand();
                saec.returnQueueName = queueName;
                saec.StartTime = DateTime.MinValue;
                saec.EndTime = DateTime.MaxValue;
                sender.Publish(saec);

                Thread.Sleep(1000);

                Assert.AreEqual(1, receiver.ReceivedTestEventCount);
            }
        }

        [TestMethod]
        public void PublishEventsToCertainQueueTestFourTimes()
        {
            var repoMock = new Mock<IRepository>(MockBehavior.Strict);
            var ev = new SerializedEvent() { ID = 1337, TimeReceived = DateTime.Now, Body = "test body", EventType = typeof(TestEvent).FullName, RoutingKey = "#" };
            repoMock.Setup(repo => repo.FindBy(It.IsAny<Expression<Func<SerializedEvent, bool>>>())).Returns(() => new List<SerializedEvent>() { ev, ev, ev, ev });
            var queueName = "TestQueue2";
            using (var sender = new EventPublisher(_busOptions))
            using (var receiver = new TestDispatcher(_direct, queueName))
            using (var publisher = new AuditPublisher(_direct))
            using (var dispatcher = new AuditDispatcher(repoMock.Object, publisher, _busOptions))
            {
                var saec = new SendAllEventCommand();
                saec.returnQueueName = queueName;
                saec.StartTime = DateTime.MinValue;
                saec.EndTime = DateTime.MaxValue;
                sender.Publish(saec);

                Thread.Sleep(1000);

                Assert.AreEqual(4, receiver.ReceivedTestEventCount);
            }
        }

        [TestMethod]
        public void PublishEventsToCertainQueueWithEventBusDispatcherTest()
        {
            var testEventJson = JsonConvert.SerializeObject(new TestEvent());

            var repoMock = new Mock<IRepository>(MockBehavior.Strict);
            repoMock.Setup(repo => repo.FindBy(It.IsAny<Expression<Func<SerializedEvent, bool>>>()))
                .Returns(() => new List<SerializedEvent>() {
                    new SerializedEvent() { ID = 1337, TimeReceived = DateTime.Now, Body = testEventJson, EventType = typeof(TestEvent).FullName, RoutingKey = "#" }
                });
            var queueName = "TestQueue3";
            using (var sender = new EventPublisher(_busOptions))
            using (var receiver = new TestDispatcherImplEventDispatcher(_direct, queueName))
            using (var publisher = new AuditPublisher(_direct))
            using (var dispatcher = new AuditDispatcher(repoMock.Object, publisher, _busOptions))
            {
                var saec = new SendAllEventCommand();
                saec.returnQueueName = queueName;
                saec.StartTime = DateTime.MinValue;
                saec.EndTime = DateTime.MaxValue;
                sender.Publish(saec);

                Thread.Sleep(1000);

                Assert.AreEqual(1, receiver.ReceivedTestEventCount);
            }
        }
    }
}
