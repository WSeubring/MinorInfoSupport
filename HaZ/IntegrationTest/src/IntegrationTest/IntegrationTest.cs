using InfoSupport.WSA.Infrastructure;
using Kantilever.Catalogusbeheer.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntegrationTest
{
    [TestClass]
    public class IntegrationTest
    {
        private static StartupHelper _startupHelper;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            _startupHelper = new StartupHelper();
            Console.WriteLine("FeWebshop.mysql: " + _startupHelper.Configuration.GetConnectionString("FeWebshop"));
            Console.WriteLine("FeBestellingen.mysql: " + _startupHelper.Configuration.GetConnectionString("FeBestellingen"));
            Console.WriteLine("PsWinkelen.mysql: " + _startupHelper.Configuration.GetConnectionString("PsWinkelen"));
            Console.WriteLine("DsBestellingen.mysql: " + _startupHelper.Configuration.GetConnectionString("DsBestellingen"));
            _startupHelper.WaitForRabbitMQ();
            _startupHelper.WaitForDb("FeWebshop");
            _startupHelper.WaitForDb("FeBestellingen");
            _startupHelper.WaitForDb("PsWinkelen");
            _startupHelper.WaitForDb("DsBestellingen");
        }

        /// <summary>
        /// Test Bestelling plaatsen
        /// Test --> [c] -> FeWinkelen --> [c] -> PsWinkelen --> [c] -> DsBestellingBeheer --> [e] -> FeBestellingen
        /// </summary>
        [TestMethod]
        public void TestBestellingPlaatsen()
        {
            // Arrange
            // Add Artikelen to each database
            Console.WriteLine("Publish artikelen on the eventbus");
            var klant = new {
                naam = "Piet Karels",
                land = "Nederland",
                postcode = "1234ab",
                huisnummer = 15,
                straatnaam = "Hoofdstraat",
                plaats = "Utrecht"
            };
            var artikel1 = new ArtikelAanCatalogusToegevoegd()
            {
                Artikelnummer = 100100,
                Naam = "Fietslamp",
                Prijs = 20.16M
            };
            var artikel2 = new ArtikelAanCatalogusToegevoegd()
            {
                Artikelnummer = 100101,
                Naam = "Fietsbel",
                Prijs = 6.15M
            };
            var artikel3 = new ArtikelAanCatalogusToegevoegd()
            {
                Artikelnummer = 100102,
                Naam = "Ketting",
                Prijs = 29.99M
            };
            using (var eventPublisher = new EventPublisher(BusOptions.CreateFromEnvironment()))
            {
                eventPublisher.Publish(artikel1);
                eventPublisher.Publish(artikel2);
                eventPublisher.Publish(artikel3);
            }

            Console.WriteLine("Wait 5s");
            Thread.Sleep(5000);

            // Act
            using (var client = new HttpClient())
            {
                var body = new {
                    artikelen = new List<int> { 100100, 100102, 100100 },
                    klant = klant
                };
                var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                var url = _startupHelper.Configuration.GetConnectionString("WinkelenServiceUri") + "/api/v1/Bestellingen";
                Console.WriteLine("Post to "  + url + " with Body: (application/json)"  + JsonConvert.SerializeObject(body));
                var response = client.PostAsync(url, content).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Response: " + response.StatusCode + ", body (application/json)" + responseString);
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            }
        }

    }
}
