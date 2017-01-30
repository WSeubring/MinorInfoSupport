using HAZ.FeWebshop.Domain.Entities;
using HAZ.FeWebshop.Infrastructure.Repositories;
using InfoSupport.WSA.Infrastructure;
using Kantilever.Catalogusbeheer.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;

namespace HAZ.FeWebshop.Listener.Test
{
    [TestClass]
    public class CatalogDispatcherTests
    {
        public static Program listeners;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            WaitForMysql();
            WaitForRabbitMQ();
            Console.WriteLine("Start Listener");
            listeners = new Program();
            listeners.Start(false);
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            listeners.StopDispatchers();
        }

        [TestMethod]
        public void IntergratietestArtikelAanCatalogusToegevoegd()
        {
            Console.WriteLine("Start ArtikelAanCatalogusToegevoegd");

            // Arrange
            var artikelAanCatalogusToegevoegd = new ArtikelAanCatalogusToegevoegd
            {
                AfbeeldingUrl = "/image.jpg",
                Artikelnummer = 2000,
                Beschrijving = "Blauwe Fietsbel",
                Naam = "Fietsbel",
                Prijs = 100M,
                Leverancier = "FietsBouwer",
                LeverancierCode = "FB",
                LeverbaarVanaf = new DateTime(2017, 1, 1),
                LeverbaarTot = null
            };

            var busoptions = BusOptions.CreateFromEnvironment();
            using (var publisher = new EventPublisher(busoptions))
            {
                // Act
                publisher.Publish(artikelAanCatalogusToegevoegd);
            }
            //Wait for listener to handle event
            Thread.Sleep(1000);

            var testConfig = new TestConfig();
            using (var artikelRepo = new ArtikelRepository(new WebshopContext(testConfig.GetDbContext())))
            {
                //Assert
                var artikelen = artikelRepo.FindAll();
                var artikel = artikelen.First();
                
                Assert.AreEqual(artikel.AfbeeldingUrl, artikelAanCatalogusToegevoegd.AfbeeldingUrl);
                Assert.AreEqual(artikel.Artikelnummer, artikelAanCatalogusToegevoegd.Artikelnummer);
                Assert.AreEqual(artikel.Beschrijving, artikelAanCatalogusToegevoegd.Beschrijving);
                Assert.AreEqual(artikel.Naam, artikelAanCatalogusToegevoegd.Naam);
                Assert.AreEqual(artikel.Prijs, artikelAanCatalogusToegevoegd.Prijs);
                Assert.AreEqual(artikel.Leverancier, artikelAanCatalogusToegevoegd.Leverancier);
                Assert.AreEqual(artikel.LeverancierCode, artikelAanCatalogusToegevoegd.LeverancierCode);
                Assert.AreEqual(artikel.LeverbaarVanaf, artikelAanCatalogusToegevoegd.LeverbaarVanaf);
                Assert.AreEqual(artikel.LeverbaarTot, artikelAanCatalogusToegevoegd.LeverbaarTot);
            }
        }

        [TestMethod]
        public void IntergratietestArtikelUitCatalogusVerwijderd()
        {
            Console.WriteLine("Start ArtikelUitCatalogusVerwijderd");

            // Arrange
            var testConfig = new TestConfig();
            int artikelnummer = 3000;
            Artikel newArtikel = new Artikel
            {
                Artikelnummer = artikelnummer,
                Naam = "Fietsbel",
                Beschrijving = "Blauwe Fietsbel",
                Prijs = 100M,
                AfbeeldingUrl = "image.jpg",
                LeverbaarVanaf = new DateTime(2017, 1, 1),
                LeverbaarTot = null,
                LeverancierCode = "FB",
                Leverancier = "FietsBouwer",
                InCatalog = true
            };

            using (var artikelRepo = new ArtikelRepository(new WebshopContext(testConfig.GetDbContext())))
            {
                var artikelen = artikelRepo.Insert(newArtikel);
            }

            var artikelUitCatalogusVerwijderd = new ArtikelUitCatalogusVerwijderd
            {
                Artikelnummer = 3000
            };

            var busoptions = BusOptions.CreateFromEnvironment();
            using (var publisher = new EventPublisher(busoptions))
            {
                // Act
                publisher.Publish(artikelUitCatalogusVerwijderd);
            }
            //Wait for listener to handle event
            Thread.Sleep(1000);

            using (var artikelRepo = new ArtikelRepository(new WebshopContext(testConfig.GetDbContext())))
            {
                //Assert
                var foundArtikel = artikelRepo.Find(artikelnummer);
                Assert.IsNull(foundArtikel);
            }
        }
        private static void WaitForRabbitMQ()
        {
            bool online = false;
            Console.WriteLine("HAZ.FeWebshop.Api.Test: Waiting for rabbit...");

            int tryLimit = 60;
            int tryCount = 0;
            while (!online)
            {
                try
                {
                    Console.WriteLine("HAZ.FeWebshop.Listener.Test: Polling rabbit...");
                    using (var publisher = new EventPublisher(BusOptions.CreateFromEnvironment()))
                    {
                        Console.WriteLine("HAZ.FeWebshop.Listener.Test: rabbit is online!");
                        online = true;
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine("HAZ.FeWebshop.Listener.Test: rabbit is still offline, retry in 1s");
                    Thread.Sleep(1000);
                    tryCount++;
                    if (tryCount >= tryLimit)
                    {
                        Console.WriteLine("HAZ.FeWebshop.Listener.Test: rabbit is still offline after 60s, stop polling ");
                        break;
                    }
                }
            }
        }

        private static void WaitForMysql()
        {
            var testConfig = new TestConfig();
            bool online = false;
            Console.WriteLine("HAZ.FeWebshop.Api.Test: Waiting for mysql...");

            int tryLimit = 60;
            int tryCount = 0;
            while (!online)
            {
                try
                {
                    Console.WriteLine("HAZ.FeWebshop.Listener.Test: Polling mysql...");
                    using (var artikelRepo = new ArtikelRepository(new WebshopContext(testConfig.GetDbContext())))
                    {
                        Console.WriteLine("HAZ.FeWebshop.Listener.Test: mysql is online!");
                        online = true;
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine("HAZ.FeWebshop.Listener.Test: mysql is still offline, retry in 1s");
                    Thread.Sleep(1000);
                    tryCount++;
                    if (tryCount >= tryLimit)
                    {
                        Console.WriteLine("HAZ.FeWebshop.Listener.Test: mysql is still offline after 60s, stop polling ");
                        break;
                    }
                }
            }
        }
    }
}
