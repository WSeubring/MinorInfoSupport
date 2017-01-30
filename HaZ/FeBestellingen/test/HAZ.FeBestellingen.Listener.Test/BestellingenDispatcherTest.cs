using HAZ.FeBestellingen.Listener.EventListener;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HAZ.FeBestellingen.Infrastructure.Repositories;
using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HaZ.DSBestellingenBeheer.Outgoing.Events;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;

namespace HAZ.FeBestellingen.Listener.Test
{
    [TestClass]
    public class BestellingenDispatcherTest
    {
        public IConfigurationRoot Configuration { get; private set; }

        private LoggerFactory InitializeLoggerFactory()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"./appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            
            var loggerFactory = new LoggerFactory();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.ConfigurationSection(Configuration.GetSection("SerilogDebug"))
                .ReadFrom.ConfigurationSection(Configuration.GetSection("SerilogError"))
                .CreateLogger();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog(Log.Logger);
                        
            return loggerFactory;
        }        

        [TestMethod]
        public void DispatcherAddsBestellingToRepository()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<BestellingenContext>()
                    .UseSqlite(connection)
                    .Options;
                BestellingToegevoegdEvent bestellingToegevoegdEvent = CreateGoodBestellingToegevoegdEvent();
                var busoptions = new BusOptions();
                var logger = InitializeLoggerFactory();

                // Act
                using (var dispatcher = new BestellingenDispatcher(busoptions, options, logger))
                {
                    dispatcher.BestellingToegevoegdEventReceived(bestellingToegevoegdEvent);
                }

                // Assert
                using (var context = new BestellingenContext(options))
                {
                    Assert.AreEqual(1, context.Bestellingen.Count());
                    var result = context.Bestellingen.Include(b => b.Klantgegevens).FirstOrDefault();
                    Assert.IsNotNull(result);
                    Assert.AreEqual(1, result.Bestelnummer);
                    Assert.IsNotNull(result.Klantgegevens);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        private static BestellingToegevoegdEvent CreateGoodBestellingToegevoegdEvent()
        {
            var bestellingToegevoegdEvent = new BestellingToegevoegdEvent();
            bestellingToegevoegdEvent.Bestelnummer = 1;
            bestellingToegevoegdEvent.DatumBestelling = DateTime.UtcNow;
            bestellingToegevoegdEvent.TotaalBedragInc = 60.0m;
            bestellingToegevoegdEvent.TotaalBedragExc = 30.0m;
            bestellingToegevoegdEvent.BestelStatus = "Testfase";
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
            bestelregel.ArtikelBeschrijving = "Een bel om mee te tringelingen";
            bestelregel.PrijsPerArtikelInc = 20.0m;
            bestelregel.PrijsPerArtikelExc = 10.0m;
            bestelregel.LeverancierCode = "Info Support Bellen bv.";
            bestelregel.AfbeeldingUrl = "Http://Image.Google.com/Fietsbel";

            bestelregels.Add(bestelregel);
            bestellingToegevoegdEvent.Bestelregels = bestelregels;
            return bestellingToegevoegdEvent;
        }
    }
}
