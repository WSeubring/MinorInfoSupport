using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Minor.Case1.AdministratieCursusenCursistenApi.DAL.Interfaces;
using Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten;

namespace Minor.Case1.AdministratieCursusenCursistenApiTest
{
    internal class MockCursusInstantieRepository : IRepository<CursusInstantie, long>
    {
        public int AantalCallsOpFindAll { get; private set; }
        public int AantalCallsFindBy { get; internal set; }

        public List<IEnumerable<CursusInstantie>> CallsOpAddRange = new List<IEnumerable<CursusInstantie>>();

        public MockCursusInstantieRepository()
        {
        }

        public IEnumerable<CursusInstantie> FindAll()
        {
            AantalCallsOpFindAll++;
            return new List<CursusInstantie>()
            {
                new CursusInstantie()
                {
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

        public void AddRange(IEnumerable<CursusInstantie> cursusInstanties)
        {
            CallsOpAddRange.Add(cursusInstanties);
        }

        public IEnumerable<CursusInstantie> FindBy(Expression<Func<CursusInstantie, bool>> filter)
        {
            AantalCallsFindBy++;
            return new List<CursusInstantie>();
        }
    }
}