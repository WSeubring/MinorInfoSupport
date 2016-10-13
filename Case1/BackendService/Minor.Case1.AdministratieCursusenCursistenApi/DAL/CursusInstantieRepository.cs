using Microsoft.EntityFrameworkCore;
using Minor.Case1.AdministratieCursusenCursistenApi.DAL.Interfaces;
using Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Minor.Case1.AdministratieCursusenCursistenApi.DAL
{
    public class CursusInstantieRepository : IRepository<CursusInstantie, long>
    {
        private AdministratieCursusenCuristenContext _context;

        public CursusInstantieRepository(AdministratieCursusenCuristenContext context)
        {
            _context = context;
        }

        public void AddRange(IEnumerable<CursusInstantie> cursusInstanties)
        {
            _context.AddRange(cursusInstanties);
            _context.SaveChanges();
        }

        public IEnumerable<CursusInstantie> FindAll()
        {
            return _context.CursusInstanties.Include(ci => ci.Cursus).OrderBy(ci => ci.StartDatum).ToList();
        }
        public IEnumerable<CursusInstantie> FindBy(Expression<Func<CursusInstantie, bool>> filter)
        {
            return _context.CursusInstanties.Include(ci => ci.Cursus).Where(filter).OrderBy(ci => ci.StartDatum).ToList();
        }
    }
}
