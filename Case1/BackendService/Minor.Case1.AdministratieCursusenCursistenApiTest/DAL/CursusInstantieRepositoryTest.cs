using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case1.AdministratieCursusenCursistenApi.Controllers;
using Minor.Case1.AdministratieCursusenCursistenApi.DAL;
using Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Case1.AdministratieCursusenCursistenApiTest.DAL
{
    [TestClass]
    public class CursusInstantieRepositoryTest
    {
        private DbContextOptions _options;

        [TestInitialize]
        public void TestInit()
        {
            _options = CreateNewContextOptions();
        }
        private static DbContextOptions<AdministratieCursusenCuristenContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<AdministratieCursusenCuristenContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }


        [TestMethod]
        public void GetMetLegeLijstVanCursusInstanties()
        {
            using (var context = new AdministratieCursusenCuristenContext(_options))
            {
                //Arrange
                var target = new CursusInstantieController(new CursusInstantieRepository(context));

                //Act
                var result = target.Get();

                //Assert
                Assert.AreEqual(0, result.Count());
            }
        }

        [TestMethod]
        public void Get1Item()
        {
            //Arrange
            var cursusInstantie = new CursusInstantie()
            {
                CursusCode="TESTCURSUS",
                StartDatum = new DateTime(2016, 10, 10),
                Cursus = new Cursus()
                {
                    Code = "TESTCURSUS"
                }
            };


            using (var context = new AdministratieCursusenCuristenContext(_options))
            {
                context.CursusInstanties.Add(cursusInstantie);
                context.SaveChanges();
            }

            using (var context = new AdministratieCursusenCuristenContext(_options))
            {
                var target = new CursusInstantieController(new CursusInstantieRepository(context));

                //Act
                var result = target.Get();

                //Assert
                Assert.AreEqual(new DateTime(2016, 10, 10), result.Single().StartDatum);
                Assert.AreEqual("TESTCURSUS", result.Single().Cursus.Code);
            }
        }

        [TestMethod]
        public void Get2ItemsOrderd()
        {
            //Arrange
            var cursusInstantie = new CursusInstantie()
            {
                CursusCode = "TESTCURSUS",
                StartDatum = new DateTime(2016, 10, 10),
                Cursus = new Cursus()
                {
                    Code = "TESTCURSUS"
                }
            };
            var cursusInstantie2 = new CursusInstantie()
            {
                CursusCode = "TESTCURSUS2",
                StartDatum = new DateTime(2016, 1, 1),
                Cursus = new Cursus()
                {
                    Code = "TESTCURSUS2"
                }
            };

            using (var context = new AdministratieCursusenCuristenContext(_options))
            {
                context.CursusInstanties.Add(cursusInstantie);
                context.CursusInstanties.Add(cursusInstantie2);
                context.SaveChanges();
            }

            using (var context = new AdministratieCursusenCuristenContext(_options))
            {
                var target = new CursusInstantieController(new CursusInstantieRepository(context));

                //Act
                var result = target.Get();

                //Assert
                Assert.IsTrue(result.OrderBy(ci => ci.StartDatum).SequenceEqual(result));
            }
        }
    }
}
