using Lapiwe.GMS.FrontEnd.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lapiwe.GMS.FrontEnd.Enitities;
using Microsoft.Extensions.DependencyInjection;

namespace Lapiwe.GMS.FrontEnd.Test
{
    [TestClass]
    public class KlantRepositoryTests
    {
        private DbContextOptions _options = CreateNewContextOptions();

        [TestMethod]
        public void FindWithExistingId()
        {
            // Arrange
            var klant = DefaultKlant();
            klant.ID = 1;

            Klant result;
            using (var context = new KlantContext(_options))
            {
                context.Klanten.Add(klant);
                context.SaveChanges();

                using (var repository = new KlantRepository(context))
                {
                    // Act
                    result = repository.Find(1);
                }
            }

            // Assert
            Assert.AreEqual(klant,result);
        }
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

        private Klant DefaultKlant()
        {
            return new Klant()
            {
                ID = 1,
                Voornaam = "VoornaamTest",
                Tussenvoegsel = "de",
                Achternaam = "AchternaamTest",
                Emailadres = "emailadress@mail.com",
                Huisnummer = "12",
                Postcode = "1234AB",
                Straatnaam = "StraatPlein",
                Telefoonnummer = "0612345678",
                Woonplaats = "Dropplaats"
            };
        }
    }
}
