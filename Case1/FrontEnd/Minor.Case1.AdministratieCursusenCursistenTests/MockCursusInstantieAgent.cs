using System;
using System.Collections.Generic;
using Minor.Case1.AdministratieCursusenCursisten.Agents;
using Minor.Case1.AdministratieCursusenCursisten.Agents.Models;

namespace Minor.Case1.AdministratieCursusenCursistenTests
{
    internal class MockCursusInstantieAgent : ICursusInstantieAgentWrapper
    {
        public int AantalCallsOpAddFromFile { get; set; }
        public int AantalCallsOpGet { get; internal set; }

        public MockCursusInstantieAgent()
        {
        }

        public void AddFromTextFile(string text)
        {
            AantalCallsOpAddFromFile++;
        }

        public IEnumerable<CursusInstantie> Get()
        {
            AantalCallsOpGet++;
            return new List<CursusInstantie>()
            {
                new CursusInstantie()
                {
                    CursusCode="Test",
                    StartDatum= new DateTime(2016, 1, 1),
                    Cursus = new Cursus()
                    {
                        Duur = 5,
                        Code = "Test",
                        Titel = "Titel"
                    }
                }
            };
        }
    }
}