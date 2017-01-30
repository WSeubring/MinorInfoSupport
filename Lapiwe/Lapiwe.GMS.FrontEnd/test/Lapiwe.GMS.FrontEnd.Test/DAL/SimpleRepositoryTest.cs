using Lapiwe.GMS.FrontEnd.DAL;
using Lapiwe.GMS.FrontEnd.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.GMS.FrontEnd.Test.DAL
{
    [TestClass]
    public class SimpleRepositoryTest
    {

        [TestMethod]
        public void SimpleRepositoryIsDisposable()
        {
            // Arrange
            FrontendContext context = new FrontendContext(CreateNewContextOptions());
            using (SimpleRepository repository = new SimpleRepository(context))
            {
                // Assert
                Assert.IsInstanceOfType(repository, typeof(IDisposable));
            }
        }

        [TestMethod]
        public void SimpleRepositoryInitiallyReturnsEmptyListOnFindAll()
        {
            // Arrange
            FrontendContext context = new FrontendContext(CreateNewContextOptions());
            using (SimpleRepository repository = new SimpleRepository(context))
            {
                // Act
                IEnumerable<OnderhoudsOpdracht> onderhoudsOpdrachten = repository.AlleOnderhoudsOpdrachten();

                // Assert
                Assert.AreEqual(0, onderhoudsOpdrachten.Count());
            }
        }

        [TestMethod]
        public void SimpleRepositoryReturnsAllItemsAfterItemsAreAdded()
        {
            // Arrange
            DbContextOptions<FrontendContext> options = CreateNewContextOptions();
            using (SimpleRepository repository = new SimpleRepository(new FrontendContext(options)))
            {
                // Act
                repository.Add(new Auto("56-DFG-8", 678));
                repository.Add(new Auto("56-DFT-8", 679));
            }
            using (SimpleRepository repository = new SimpleRepository(new FrontendContext(options)))
            {
                // Assert
                Assert.AreEqual(2, repository.LazyLoadAll<Auto>().Count());
            }
        }

        [TestMethod]
        public void InsertingOneEntityTypeDoesNotImpactAnother()
        {
            // Arrange
            DbContextOptions<FrontendContext> options = CreateNewContextOptions();
            using (SimpleRepository repository = new SimpleRepository(new FrontendContext(options)))
            {
                // Act
                repository.Add(new Auto("56-DFG-8", 678));
                repository.Add(new Auto("56-DFT-8", 679));
                repository.Add(new Klant("Elon Musk", "(+44) 4 657 23 674"));
            }
            using (SimpleRepository repository = new SimpleRepository(new FrontendContext(options)))
            {
                // Assert
                Assert.AreEqual(2, repository.LazyLoadAll<Auto>().Count());
                Assert.AreEqual(1, repository.LazyLoadAll<Klant>().Count());
            }
        }

        [TestMethod]
        public void EntityCanBeFoundWithItsGuid()
        {
            // Arrange
            Guid guid = Guid.NewGuid();
            Auto auto = new Auto("56-DFG-8", 678);

            DbContextOptions<FrontendContext> options = CreateNewContextOptions();
            using (SimpleRepository repository = new SimpleRepository(new FrontendContext(options)))
            {
                // Act
                auto.Guid = guid;
                repository.Add(auto);
            }
            using (SimpleRepository repository = new SimpleRepository(new FrontendContext(options)))
            {
                // Assert
                Auto gevondenAuto = repository.LazyLoadFind<Auto>(guid);
                Klant nietGevondenKlant = repository.LazyLoadFind<Klant>(guid);

                Assert.AreEqual("56-DFG-8", gevondenAuto.Kenteken);
                Assert.AreEqual(678, gevondenAuto.Kilometerstand);
                Assert.IsNull(nietGevondenKlant);
            }
        }

        private DbContextOptions<FrontendContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<FrontendContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}