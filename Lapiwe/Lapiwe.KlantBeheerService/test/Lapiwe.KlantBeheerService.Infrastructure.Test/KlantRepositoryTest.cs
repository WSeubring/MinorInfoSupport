using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lapiwe.KlantBeheerService.Domain;

namespace Lapiwe.KlantBeheerService.Infrastructure.Test
{
    [TestClass]
    public class KlantRepositoryTest
    {

        private static DbContextOptions<KlantContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<KlantContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [TestMethod]
        public void Repository_InsertsKlant()
        {
            // Arrange
            var options = CreateNewContextOptions();
            IRepository target = new KlantRepository(options);

            var klant = new Klant() { };

            // Act
            target.Insert(klant);

            // Assert
            using (var context = new KlantContext(options))
            {
                Assert.AreEqual(1, context.Klanten.Count());
            }
        }
    }
}
