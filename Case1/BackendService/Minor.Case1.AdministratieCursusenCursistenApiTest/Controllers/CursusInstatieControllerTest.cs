using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case1.AdministratieCursusenCursistenApi.Controllers;

namespace Minor.Case1.AdministratieCursusenCursistenApiTest
{
    [TestClass]
    public class CursusInstatieControllerTest
    {
        
        [TestMethod]
        public void Get()
        {
            //Arrange
            var mockRepository = new MockCursusInstantieRepository();
            var target = new CursusInstantieController(mockRepository);

            //Act
            target.Get();

            //Assert
            Assert.AreEqual(1, mockRepository.AantalCallOpFindAll);

        }
    }
}
