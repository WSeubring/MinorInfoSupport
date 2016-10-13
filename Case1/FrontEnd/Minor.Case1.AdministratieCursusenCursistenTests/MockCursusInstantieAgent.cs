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
        public int AantalCallsOpGetMetJaarEnWeek { get; private set; }
        public int LaatstMeegegeveJaarInGet { get; private set; }
        public int LaatstMeegegeveWeekInGet { get; private set; }

        public MockCursusInstantieAgent()
        {
        }

        public AddFromFileResultReport AddFromTextFile(string text)
        {
            AantalCallsOpAddFromFile++;
            return new AddFromFileResultReport();
        }

        public IEnumerable<CursusInstantie> Get()
        {
            AantalCallsOpGet++;
            return new List<CursusInstantie>()
            {
                new CursusInstantie()
                {
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

        public IEnumerable<CursusInstantie> Get(int jaar, int week)
        {
            AantalCallsOpGetMetJaarEnWeek++;
            LaatstMeegegeveJaarInGet = jaar;
            LaatstMeegegeveWeekInGet = week;

            return new List<CursusInstantie>();
        }
    }
}