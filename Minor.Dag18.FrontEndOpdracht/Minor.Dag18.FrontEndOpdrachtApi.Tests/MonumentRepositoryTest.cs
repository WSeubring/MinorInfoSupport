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
        private DbContextOptions<MonumentContext> _options = CreateNewContextOptions();

        [TestMethod]
        public void AddMonument()
        {
            //Arrange
            using (var context = new MonumentContext(_options))
            {
                var target = new MonumentRepository(context);
                
                //Act
                target.Add(new Monument() { ID = 10, Naam = "Monument Test" });
                
            }

            using (var context = new MonumentContext(_options))
            {
                //Assert
                Assert.AreEqual(10, context.Monumenten.Single(m => m.ID == 10).ID);
                Assert.AreEqual("Monument Test", context.Monumenten.Single().Naam);
            }
        }

        [TestMethod]
        public void AddExistingMonument()
        {
            //Arrange
            using (var context = new MonumentContext(_options))
            {
                var monument = new Monument() { ID = 123, Naam = "Notre Test" };
                context.Monumenten.Add(monument);
                context.SaveChanges();

                var target = new MonumentRepository(context);

                //Act
                Action action = () => target.Add(monument);

                //Assert
                Assert.ThrowsException<ArgumentException>(action);
            }
        }

        [TestMethod]
        public void DeleteMonument()
        {
            //Arrange
            using (var context = new MonumentContext(_options))
            {
                context.Monumenten.Add(new Monument() { ID = 123, Naam = "Notre Test" });
                context.SaveChanges();

                var target = new MonumentRepository(context);

                //Act
                target.Delete(123);
            }

            using (var context = new MonumentContext(_options))
            {
                Assert.IsNull(context.Monumenten.Where(m => m.ID == 123).FirstOrDefault());
            }
        }
        public void DeleteNonExistingMonument()
        {
            //Arrange
            using (var context = new MonumentContext(_options))
            {
                var target = new MonumentRepository(context);

                //Act
                Action action = () => target.Delete(123);

                //Assert
                Assert.ThrowsException<ArgumentException>(action);
            }
        }

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
    }
}
