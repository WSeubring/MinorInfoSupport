using Lapiwe.Common.Domain;
using Lapiwe.GMS.FrontEnd.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.GMS.FrontEnd.DAL
{
    public class SimpleRepository : IDisposable, ISimpleRepository
    {
        private FrontendContext _context;

        public SimpleRepository(FrontendContext context)
        {
            _context = context;
        }

        public TEntity LazyLoadFind<TEntity>(Guid guid) where TEntity : DomainEntity
        {
            return _context.Set<TEntity>().FirstOrDefault(entity => entity.Guid == guid);
        }

        public IEnumerable<TEntity> LazyLoadAll<TEntity>() where TEntity : DomainEntity
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : DomainEntity
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<OnderhoudsOpdracht> AlleOnderhoudsOpdrachten()
        {
            return _context.OnderhoudsOpdrachten
                    .Include(o => o.Klant)
                    .Include(o => o.Auto)
                    .ToList();
        }

        public OnderhoudsOpdracht VindOnderhoudsOpdracht(Guid guid)
        {
            return _context.OnderhoudsOpdrachten
                    .Include(o => o.Auto)
                    .Include(o => o.Klant)
                    .First(entity => entity.Guid == guid);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
