using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Enities;
using Microsoft.Extensions.DependencyInjection;
using Minor.Dag19.DAL;

namespace Minor.Dag18.FrontEndOpdrachtApi.Tests
{
    [TestClass]
    public class MonumentRepositoryTest
    {
        private object context;
        private static DbContextOptions<MonumentContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<MonumentContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [TestMethod]
        public void AddMonument()
        {
            //Arrange
            var options = CreateNewContextOptions();
            var contex = new MonumentContext(options);
            var target = new MonumentRepository(context);

            //Act
            target.Insert(new Monument(){ ID = 10, Naam = "Monument Test" });

            //Assert
            Assert.AreEqual(10, contex.Monumenten.First().ID);
            Assert.AreEqual("Monument Test", contex.Monumenten.First().Naam);
        }

    }
}
