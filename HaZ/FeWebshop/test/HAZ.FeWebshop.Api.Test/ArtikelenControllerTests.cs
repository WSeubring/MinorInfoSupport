using HAZ.FeWebshop.Domain.Entities;
using HAZ.FeWebshop.Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace HAZ.FeWebshop.Api.Test
{
    [TestClass]
    public class ArtikelenControllerTests
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            var testConfig = new TestConfig();
            bool online = false;
            int maxRetries = 60;
            int retry = 0;
            Console.WriteLine("HAZ.FeWebshop.Api.Test: Waiting for mysql...");
            while (!online)
            {
                try
                {
                    Console.WriteLine("HAZ.FeWebshop.Api.Test: Polling mysql...");
                    using (var artikelRepo = new ArtikelRepository(new WebshopContext(testConfig.GetDbContext())))
                    {
                        Console.WriteLine("HAZ.FeWebshop.Api.Test: mysql is online!");
                        online = true;
                    }
                }
                catch (Exception e)
                {
                    retry++;
                    if (retry >= maxRetries)
                    {
                        throw new Exception("Cannot open mysql connection", e);
                    }
                    else
                    {
                        Console.WriteLine("HAZ.FeWebshop.Api.Test: mysql is still offline, retry in 1s");
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        [TestMethod]
        public void TestGetArtikelen()
        {
            Console.WriteLine("Start TestGetArtikelen");
            // Arrange
            var testConfig = new TestConfig();
            var artikel1 = new Artikel()
            {
                AfbeeldingUrl = "/image.jpg",
                Artikelnummer = 1238732456,
                Beschrijving = "Blauwe Fietsbel",
                Naam = "Fietsbel",
                Prijs = 15.6M
            };
            var artikel2 = new Artikel()
            {
                AfbeeldingUrl = "/image2.jpg",
                Artikelnummer = 897515,
                Beschrijving = "Gele Fietlamp",
                Naam = "Fietlamp",
                Prijs = 6.57M
            };

            // Insert Artikelen
            Console.WriteLine("Insert Artikelen");
            using (var artikelRepo = new ArtikelRepository(new WebshopContext(testConfig.GetDbContext())))
            {
                artikelRepo.Insert(artikel1);
                artikelRepo.Insert(artikel2);
            }

            // Setup Api
            Console.WriteLine("Start HAZ.FeWebshop.Api");
            var backendServer = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            var backendClient = backendServer.CreateClient();

            // Act
            Console.WriteLine("Send Request: GET /api/artikelen");
            var response = backendClient.GetAsync("/api/artikelen").Result;

            // Assert
            response.EnsureSuccessStatusCode();
            Console.WriteLine("Response: " + response.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string json = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Body: " + json);

            List<Artikel> result = JsonConvert.DeserializeObject<List<Artikel>>(json);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Fietsbel", result[1].Naam);
            Assert.AreEqual("/image.jpg", result[1].AfbeeldingUrl);
            Assert.AreEqual("Blauwe Fietsbel", result[1].Beschrijving);
            Assert.AreEqual(1238732456, result[1].Artikelnummer);
            Assert.AreEqual(15.6M, result[1].Prijs);
            Assert.AreEqual("Fietlamp", result[0].Naam);
            Assert.AreEqual("/image2.jpg", result[0].AfbeeldingUrl);
            Assert.AreEqual(897515, result[0].Artikelnummer);
            Assert.AreEqual(6.57M, result[0].Prijs);
            Assert.AreEqual("Gele Fietlamp", result[0].Beschrijving);

        }
    }
}
