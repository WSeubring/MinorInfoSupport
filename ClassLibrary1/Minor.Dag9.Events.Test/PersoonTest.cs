using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Minor.Dag9.Events.Test
{
    [TestClass]
    public class PersoonTest
    {
        [TestMethod]
        public void NieuwPersoon()
        {
            //Act
            var target = new Persoon("Test", 0);

            //Assert
            Assert.AreEqual("Test", target.Naam);
            Assert.AreEqual(0, target.Leeftijd);        
        }

        [TestMethod]
        public void PersoonWordEenJaarOuderOpVerjaardag()
        {
            //Act
            var target = new Persoon("Test", 0);

            //Arrange
            target.Verjaar();

            //Assert
            Assert.AreEqual(1, target.Leeftijd);
        }

        [TestMethod]
        public void PersoonRaiseEventOnLeeftijdChangedBijWijzigingLeeftijd()
        {
            //Act
            var target = new Persoon("Test", 0);
            var mockLeeftijdChanged = new MockLeeftijdChanged();
            target.LeeftijdChanged += mockLeeftijdChanged.LeeftijdChangeHandeled;

            //Arrange
            target.Leeftijd = 1;

            //Assert
            Assert.IsTrue(mockLeeftijdChanged.LeeftijdChangedHasBeenCalled);
        }

        [TestMethod]
        public void PersoonLeeftijdChangedEventBevatJuisteDataNaVerjaar()
        {
            //Act
            var target = new Persoon("Test", 10);
            var mockLeeftijdChanged = new MockLeeftijdChanged();
            target.LeeftijdChanged += mockLeeftijdChanged.LeeftijdChangeHandeled;

            //Arrange
            target.Verjaar();

            //Assert
            Assert.AreEqual("Test", mockLeeftijdChanged.e.Naam);
            Assert.AreEqual(10, mockLeeftijdChanged.e.OudeLeeftijd);
            Assert.AreEqual(11, mockLeeftijdChanged.e.NieuweLeeftijd);
        }

        [TestMethod]
        public void PersoonLeeftijdChangedEventBevatJuisteDataNaLeeftijdVerandering()
        {
            //Act
            var target = new Persoon("TestNaam", 10);
            var mockLeeftijdChanged = new MockLeeftijdChanged();
            target.LeeftijdChanged += mockLeeftijdChanged.LeeftijdChangeHandeled;

            //Arrange
            target.Leeftijd = 100;

            //Assert
            Assert.AreEqual("TestNaam", mockLeeftijdChanged.e.Naam);
            Assert.AreEqual(10, mockLeeftijdChanged.e.OudeLeeftijd);
            Assert.AreEqual(100, mockLeeftijdChanged.e.NieuweLeeftijd);
        }
    }
}
