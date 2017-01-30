using HaZ.DSBestellingenBeheer.Outgoing.Events;
using HAZ.FeBestellingen.Infrastructure.Repositories;
using HAZ.FeBestellingen.Listener.EventListener;
using InfoSupport.WSA.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HAZ.FeBestellingen.Listener.Integratietest
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
        public void DispatcherAddsBestellingToRepository()
        {
            // Arrange
            var expectedTime = DateTime.UtcNow;
            BestellingToegevoegdEvent bestellingToegevoegdEvent = CreateGoodBestellingToegevoegdEvent(expectedTime);

            var busoptions = BusOptions.CreateFromEnvironment();
            using (var publisher = new EventPublisher(busoptions))
            {
                // Act
                publisher.Publish(bestellingToegevoegdEvent);
            }
            //Wait for listener to handle event
            Thread.Sleep(1000);

            var testConfig = new TestConfig();
            using (var bestellingRepo = new BestellingRepository(new BestellingenContext(testConfig.GetDbContext())))
            {
                //Assert
                var result = bestellingRepo.GetBestellingByID(1);
                var klantGegevens = result.Klantgegevens;
                var bestelregels = result.Bestelregels;

                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Bestelnummer);
                Assert.AreEqual(expectedTime, result.DatumBestelling);
                Assert.AreEqual(60.0M, result.TotaalBedragInc);
                Assert.AreEqual(30.0M, result.TotaalBedragExc);
                Assert.AreEqual("Geplaatst", result.BestelStatus);

                Assert.IsNotNull(klantGegevens);
                Assert.AreEqual(1234, klantGegevens.KlantId);
                Assert.AreEqual("Klaas", klantGegevens.Naam);
                Assert.AreEqual("Nederland", klantGegevens.Land);
                Assert.AreEqual("1234AB", klantGegevens.Postcode);
                Assert.AreEqual("Kalverstraat", klantGegevens.Straatnaam);
                Assert.AreEqual("Amsterdam", klantGegevens.Woonplaats);
                Assert.AreEqual("932", klantGegevens.Huisnummer);

                Assert.IsNotNull(bestelregels);
                var bestelregel = bestelregels.FirstOrDefault();
                Assert.IsNotNull(bestelregel);
                Assert.AreEqual(666, bestelregel.ArtikelId);
                Assert.AreEqual(3, bestelregel.AantalArtikelen);
                Assert.AreEqual("Fietsbel", bestelregel.ArtikelNaam);
                Assert.AreEqual("Een bel voor op uw fiets", bestelregel.ArtikelBeschrijving);
                Assert.AreEqual(20.0m, bestelregel.PrijsPerArtikelInc);
                Assert.AreEqual(10.0m, bestelregel.PrijsPerArtikelExc);
                Assert.AreEqual("Bellen bv.", bestelregel.LeverancierCode);
                Assert.AreEqual("http://Image.Google.com/Fietsbel", bestelregel.AfbeeldingUrl);
            }
        }

        private static BestellingToegevoegdEvent CreateGoodBestellingToegevoegdEvent(DateTime time)
        {
            var bestellingToegevoegdEvent = new BestellingToegevoegdEvent();
            bestellingToegevoegdEvent.Bestelnummer = 1;
            bestellingToegevoegdEvent.DatumBestelling = time;
            bestellingToegevoegdEvent.TotaalBedragInc = 60.0m;
            bestellingToegevoegdEvent.TotaalBedragExc = 30.0m;
            bestellingToegevoegdEvent.BestelStatus = "Geplaatst";
            bestellingToegevoegdEvent.Klantgegevens = new Klantgegevens()
            {
                KlantId = 1234,
                Naam = "Klaas",
                Land = "Nederland",
                Postcode = "1234AB",
                Straatnaam = "Kalverstraat",
                Woonplaats = "Amsterdam",
                Huisnummer = "932",
            };
            List<Bestelregel> bestelregels = new List<Bestelregel>();
            Bestelregel bestelregel = new Bestelregel();
            bestelregel.ArtikelId = 666;
            bestelregel.AantalArtikelen = 3;
            bestelregel.ArtikelNaam = "Fietsbel";
            bestelregel.ArtikelBeschrijving = "Een bel voor op uw fiets";
            bestelregel.PrijsPerArtikelInc = 20.0m;
            bestelregel.PrijsPerArtikelExc = 10.0m;
            bestelregel.LeverancierCode = "Bellen bv.";
            bestelregel.AfbeeldingUrl = "http://Image.Google.com/Fietsbel";

            bestelregels.Add(bestelregel);
            bestellingToegevoegdEvent.Bestelregels = bestelregels;
            return bestellingToegevoegdEvent;
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
                    using (var artikelRepo = new BestellingRepository(new BestellingenContext(testConfig.GetDbContext())))
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
