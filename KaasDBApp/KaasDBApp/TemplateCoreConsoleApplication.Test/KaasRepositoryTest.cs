using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace TemplateCoreConsoleApplication.Test
{
    [TestClass]
    public class KaasRepositoryTest
    {
        private static DbContextOptions<KaasContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<KaasContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [TestMethod]
        public void AddEenKaas()
        {
            //Arrange
            var options = CreateNewContextOptions();

            using (var context = new KaasContext(options))
            {
                var kaas = new Kaas()
                {
                    Leeftijd = 4,
                    CategorieNaam = "Jong"
                };

                //Act
                context.Add(kaas);
                context.SaveChanges();
            }

            //Assert
            using (var contex = new KaasContext(options))
            {
                Assert.AreEqual(1, contex.Kazen.Where(k => k.CategorieNaam == "Jong").Count());
            }
        }

        [TestMethod]
        public void FindById()
        {
            var options = CreateNewContextOptions();

            int? expectedId = null;
            using (var context = new KaasContext(options))
            {
                var kaas = new Kaas()
                {
                    Leeftijd = 4,
                    CategorieNaam = "Jong"
                };

                context.Add(kaas);
                context.SaveChanges();

                expectedId = kaas.Id;
            }

            //Act
            Kaas result = null;
            if (expectedId != null)
            {

                using (var context = new KaasContext())
                {
                    result = context.FindById((int)expectedId);
                }
            }

            //Assert
            Assert.IsNotNull(result);

        }
    }
}
