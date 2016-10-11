using System;
using System.Collections.Generic;
using Minor.Case1.AdministratieCursusenCursistenApi.DAL.Interfaces;
using Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten;

namespace Minor.Case1.AdministratieCursusenCursistenApiTest
{
    internal class MockCursusInstantieRepository : IRepository<CursusInstantie, long>
    {
        public int AantalCallOpFindAll { get; private set; }

        public MockCursusInstantieRepository()
        {
        }

        public IEnumerable<CursusInstantie> FindAll()
        {
            AantalCallOpFindAll++;
            return new List<CursusInstantie>()
            {
                new CursusInstantie()
                {
                    CursusCode = "Code",
                    StartDatum = new DateTime(2016, 10, 10),
                    Cursus = new Cursus()
                    {
                        Code="CODE",
                        Duur=5,
                        Titel="Mock"
                    }
                }
            };
        }
    }
}