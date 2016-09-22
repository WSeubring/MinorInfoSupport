using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Dag12.LinqQueriesOpPersonen.Test
{
    [TestClass]
    public class PersoonQueriesTest
    {
        [TestMethod]
        public void TestGetAllVoorlettersVanPersonenMetEenRInHunNaam()
        {
            //Arrange
            var personenLijst = new List<string> { "Harry", "Pier" };
            //Act
            var result = PersoonQueries.GetVoorlettersVanPersonenMetLetterInNaam(personenLijst, 'r');

            //Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestGetAllVoorlettersVanPersonenMetEenRInHunNaamExtensionSyntax()
        {
            //Arrange
            var personenLijst = new List<string> { "Harry", "Pier" };
            //Act
            var result = PersoonQueries.GetVoorlettersVanPersonenMetLetterInNaamExtensionSyntax(personenLijst, 'r');

            //Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestGetAllVoorlettersVanPersonenMetEenRInHunNaamMetHoofdLetters()
        {
            //Arrange
            var personenLijst = new List<string> { "Riet", "Robbin" };

            //Act
            var result = PersoonQueries.GetVoorlettersVanPersonenMetLetterInNaam(personenLijst, 'r');

            //Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestGetAllVoorlettersVanPersonenMetEenRInHunNaamMetHoofdLettersExtensionSyntax()
        {
            //Arrange
            var personenLijst = new List<string> { "Riet", "Robbin" };

            //Act
            var result = PersoonQueries.GetVoorlettersVanPersonenMetLetterInNaamExtensionSyntax(personenLijst, 'r');

            //Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestGetAllVoorlettersVanPersonenMetEenWInHunNaam()
        {
            //Arrange
            var personenLijst = new List<string> { "Wesley", "Bowy" };

            //Act
            var result = PersoonQueries.GetVoorlettersVanPersonenMetLetterInNaam(personenLijst, 'W');

            //Assert
            var expected = new List<string>{"W", "B" };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGetAllVoorlettersVanPersonenMetEenWInHunNaamExtensionSyntax()
        {
            //Arrange
            var personenLijst = new List<string> { "Wesley", "Bowy" };

            //Act
            var result = PersoonQueries.GetVoorlettersVanPersonenMetLetterInNaamExtensionSyntax(personenLijst, 'W');

            //Assert
            var expected = new List<string>{"W", "B" };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AantalLettersVanDeNamenDieMetEenJBegint()
        {
            //Arrange
            var personenLijst = new List<string> {  "Jordy", "Jan", "Joop","Klaas" };

            //Act
            var result = PersoonQueries.GetAantalLettersVanDeNaamVanPersonenWaarDeNaamBegintMetGegevenLetter(personenLijst, 'j');

            //Assert
            var expected = new List<int> {3, 4, 5};
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AantalLettersVanDeNamenDieMetEenJBegintComprehesionSyntax()
        {
            //Arrange
            var personenLijst = new List<string> { "Jordy", "Jan", "Joop", "Klaas" };

            //Act
            var result = PersoonQueries.GetAantalLettersVanDeNaamVanPersonenWaarDeNaamBegintMetGegevenLetterComprehesionSyntax(personenLijst, 'j');

            //Assert
            var expected = new List<int> { 3, 4, 5 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AantalPersonenMetMetDeZelfdeLengteNaamDesc()
        {
            //Arrange
            var personenLijst = new List<string> { "Jordy", "Jan", "Joop", "Klaas" };

            //Act
            var result = PersoonQueries.AantalPersonenMetMetDeZelfdeLengteNaamDesc(personenLijst);

            //Arrange
            var expected = new List<int> { 1, 1, 2 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AantalPersonenMetMetDeZelfdeLengteNaamDescExtensionSyntax()
        {
            //Arrange
            var personenLijst = new List<string> { "Jordy", "Jan", "Joop", "Klaas" };

            //Act
            var result = PersoonQueries.AantalPersonenMetMetDeZelfdeLengteNaamDescExtensionSyntax(personenLijst);

            //Arrange
            var expected = new List<int> { 1, 1, 2 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void KortsteNamenMetDeNamenMetEenAWegGelatenAlleenEenKortWordMetAComprehensionSyntax()
        {
            //Arrange
            var personenLijst = new List<string> { "At", "Kenny", "TestPersoon" };

            //Act
            var result = PersoonQueries.GetPersonenMetDeKortsteNaamZonderGegevenLetterInDeNaamComprehensionSyntax(personenLijst, 'A');

            //Assert
            var expected = new List<string> {};
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void KortsteNamenMetDeNamenMetEenAWegGelatenAlleenEenKortWordMetA()
        {
            //Arrange
            var personenLijst = new List<string> { "At", "Kenny", "TestPersoon" };

            //Act
            var result = PersoonQueries.GetPersonenMetDeKortsteNaamZonderGegevenLetterInDeNaam(personenLijst, 'A');

            //Assert
            var expected = new List<string> { };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void KortsteNamenMetDeNamenMetEenAWegGelaten3WoordenMet3LettersComprehensionSyntax()
        {
            //Arrange
            var personenLijst = new List<string> { "Pet", "Pot", "Put", "Pat", "Harry", "Henk" };

            //Act
            var result = PersoonQueries.GetPersonenMetDeKortsteNaamZonderGegevenLetterInDeNaamComprehensionSyntax(personenLijst, 'A');

            //Assert
            var expected = new List<string> { "Pet", "Pot", "Put" };
            CollectionAssert.AreEqual(expected, result);
        }
    }
};