using Lapiwe.GMS.FrontEnd.DAL.Interfaces;
using Lapiwe.GMS.FrontEnd.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Lapiwe.GMS.FrontEnd.DAL
{
    public class KlantRepository : IKlantRepository
    {
        private KlantContext _context;

        public KlantRepository(KlantContext context)
        {
            _context = context;
        }

        public void Delete(Klant item)
        {
            throw new NotImplementedException();
        }

        public Klant Find(long id)
        {
            return _context.Klanten.SingleOrDefault(klant => klant.ID == id);
        }

        public IEnumerable<Klant> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Klant> FindBy(Expression<Func<Klant, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Insert(Klant item)
        {
            _context.Add(new Klant() { Voornaam = "henk" });
            _context.SaveChanges();
        }

        public void Update(Klant item)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
