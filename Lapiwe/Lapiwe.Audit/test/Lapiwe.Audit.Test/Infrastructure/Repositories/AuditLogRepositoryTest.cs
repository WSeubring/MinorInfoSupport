using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Lapiwe.Audit.Test.Infrastructure.Context;
using Lapiwe.Audit.Domain.Contracts;
using Lapiwe.Audit.Infrastructure.Repositories;
using Lapiwe.Audit.Domain;

namespace Lapiwe.Audit.Test.Infrastructure.Repositories
{
    [TestClass]
    public class AuditLogRepositoryTest
    {
        [TestMethod]
        public void RepositoryCanBeInstantiated()
        {
            // Arrange
            DbContextOptions options = new DbContextOptionsBuilder().UseInMemoryDatabase().Options;
            InMemoryAuditLogDbContext context = new InMemoryAuditLogDbContext(options);

            // Assert
            using (IRepository repository = new AuditLogRepository(context))
            {
                Assert.IsInstanceOfType(repository, typeof(IRepository));
            }
        }

        [TestMethod]
        public void RepositoryCanInsertSerializedEvents()
        {
            // Arrange
            DbContextOptions options = new DbContextOptionsBuilder().UseInMemoryDatabase().Options;
            SerializedEvent serializedEvent = new SerializedEvent { RoutingKey = "BackendService.RoomCreated", EventType = "RoomCreatedEvent", Body = "{room: 'chess123'}" };

            // Act
            using (IRepository repository = new AuditLogRepository(new InMemoryAuditLogDbContext(options)))
            {
                repository.Insert(serializedEvent);
            }

            // Assert
            using (InMemoryAuditLogDbContext context = new InMemoryAuditLogDbContext(options))
            {
                try
                {
                    Assert.AreEqual(1, context.SerializedEvents.Count());
                } finally
                {
                    context.SerializedEvents.RemoveRange(context.SerializedEvents);
                    context.SaveChanges();
                }
            }
        }

        [TestMethod]
        public void RepositoryCanFindBy()
        {
            // Arrange
            DbContextOptions options = new DbContextOptionsBuilder().UseInMemoryDatabase().Options;
            InMemoryAuditLogDbContext context = new InMemoryAuditLogDbContext(options);
            SerializedEvent serializedEvent = new SerializedEvent { RoutingKey = "BackendService.RoomCreated", EventType = "RoomCreatedEvent", Body = "{room: 'chess123'}" };
            SerializedEvent otherSerializedEvent = new SerializedEvent { RoutingKey = "BackendService.RoomCreated", EventType = "RoomCreatedEvent", Body = "{game: 'theultimategame'}" };

            using (IRepository repository = new AuditLogRepository(new InMemoryAuditLogDbContext(options)))
            {
                repository.Insert(serializedEvent);
                repository.Insert(otherSerializedEvent);
            }

            // Assert
            using (IRepository repository = new AuditLogRepository(context))
            {
                IEnumerable<SerializedEvent> foundItems = repository.FindBy(s => s.ID == otherSerializedEvent.ID);

                try
                {
                    Assert.AreEqual(1, foundItems.Count());
                    Assert.AreEqual(otherSerializedEvent.ID, foundItems.First().ID);
                }
                finally
                {
                    context.SerializedEvents.RemoveRange(context.SerializedEvents);
                    context.SaveChanges();
                }
            }
        }
    }
}
