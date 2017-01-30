using HAZ.FeWebshop.Domain.Entities;
using HAZ.FeWebshop.Infrastructure.Repositories;
using InfoSupport.WSA.Infrastructure;
using Kantilever.Magazijnbeheer;
using Kantilever.Magazijnbeheer.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HAZ.FeWebshop.Listener.Test
{
    [TestClass]
    public class MagazijnDispatcherTests
    {
        [TestMethod]
        public void IntergratietestArtikelInMagazijnGezet()
        {
            Console.WriteLine("Start ArtikelInMagazijnGezet");

            // Arrange
            var testConfig = new TestConfig();

            int artikelnummer = 4000;
            int expectedVoorraad = 1;

            var newArtikel = new Artikel
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

            var artikelInMagazijnGezet = new ArtikelInMagazijnGezet
            {
                ArtikelID = artikelnummer,
                Voorraad = expectedVoorraad
            };

            using (var artikelRepo = new ArtikelRepository(new WebshopContext(testConfig.GetDbContext())))
            {
                artikelRepo.Insert(newArtikel);
            }

            var busoptions = BusOptions.CreateFromEnvironment();
            using (var publisher = new EventPublisher(busoptions))
            {
                // Act
                publisher.Publish(artikelInMagazijnGezet);
            }
            //Wait for listener to handle event
            Thread.Sleep(1000);

            using (var artikelRepo = new ArtikelRepository(new WebshopContext(testConfig.GetDbContext())))
            {
                //Assert
                var artikel = artikelRepo.Find(artikelnummer);

                Assert.AreEqual(artikel.Artikelnummer, artikelnummer);
                Assert.AreEqual(artikel.Voorraad, expectedVoorraad);
            }
        }

        [TestMethod]
        public void IntergratietestArtikelInMagazijnGezetWithExistingVoorraad()
        {
            Console.WriteLine("Start ArtikelInMagazijnGezet");

            // Arrange
            Console.WriteLine("Start Listener");
            var testConfig = new TestConfig();

            int artikelnummer = 5000;
            int expectedVoorraad = 25;

            var newArtikel = new Artikel
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
                InCatalog = true,
                Voorraad = 5
            };

            var artikelInMagazijnGezet = new ArtikelInMagazijnGezet
            {
                ArtikelID = artikelnummer,
                Voorraad = expectedVoorraad
            };

            using (var artikelRepo = new ArtikelRepository(new WebshopContext(testConfig.GetDbContext())))
            {
                artikelRepo.Insert(newArtikel);
            }

            var busoptions = BusOptions.CreateFromEnvironment();
            using (var publisher = new EventPublisher(busoptions))
            {
                // Act
                publisher.Publish(artikelInMagazijnGezet);
            }
            //Wait for listener to handle event
            Thread.Sleep(1000);

            using (var artikelRepo = new ArtikelRepository(new WebshopContext(testConfig.GetDbContext())))
            {
                //Assert
                var artikel = artikelRepo.Find(artikelnummer);

                Assert.AreEqual(artikel.Artikelnummer, artikelnummer);
                Assert.AreEqual(artikel.Voorraad, expectedVoorraad);
            }
        }

        [TestMethod]
        public void IntergratietestArtikelUitMagazijnGehaald()
        {
            Console.WriteLine("Start ArtikelUitMagazijnGehaald");

            // Arrange
            Console.WriteLine("Start Listener");
            var testConfig = new TestConfig();

            int artikelnummer = 6000;
            int expectedVoorraad = 4;

            var newArtikel = new Artikel
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
                InCatalog = true,
                Voorraad = 5
            };

            var artikelUitMagazijnGehaald = new ArtikelUitMagazijnGehaald
            {
                ArtikelID = artikelnummer,
                Voorraad = expectedVoorraad
            };

            using (var artikelRepo = new ArtikelRepository(new WebshopContext(testConfig.GetDbContext())))
            {
                artikelRepo.Insert(newArtikel);
            }

            var busoptions = BusOptions.CreateFromEnvironment();
            using (var publisher = new EventPublisher(busoptions))
            {
                // Act
                publisher.Publish(artikelUitMagazijnGehaald);
            }
            //Wait for listner to handle event
            Thread.Sleep(1000);

            using (var artikelRepo = new ArtikelRepository(new WebshopContext(testConfig.GetDbContext())))
            {
                //Assert
                var artikel = artikelRepo.Find(artikelnummer);

                Assert.AreEqual(artikel.Artikelnummer, artikelnummer);
                Assert.AreEqual(artikel.Voorraad, expectedVoorraad);
            }
        }
    }
}
