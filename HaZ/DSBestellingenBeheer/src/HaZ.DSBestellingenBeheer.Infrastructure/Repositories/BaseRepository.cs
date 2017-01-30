﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using HaZ.DSBestellingenBeheer.Services.Interfaces;

namespace HaZ.DSBestellingenBeheer.Infrastructure.Repositories
{
    public abstract class BaseRepository<Entity, Key, Context> : IRepository<Entity, Key>, IDisposable
        where Entity : class
        where Context : DbContext
    {
        protected Context _context;

        protected abstract DbSet<Entity> GetDbSet();
        protected abstract Key GetKeyFrom(Entity item);

        public BaseRepository(Context context)
        {
            _context = context;
        }

        public virtual IEnumerable<Entity> FindBy(Expression<Func<Entity, bool>> filter)
        {
            return GetDbSet().Where(filter);
        }

        public virtual Entity Find(Key id)
        {
            return GetDbSet().SingleOrDefault(a => GetKeyFrom(a).Equals(id));
        }

        public virtual IEnumerable<Entity> FindAll()
        {
            return GetDbSet();
        }

        public virtual int Insert(Entity item)
        {
            _context.Add(item);
            return _context.SaveChanges();
        }

        public virtual int InsertRange(IEnumerable<Entity> items)
        {
            _context.AddRange(items);
            return _context.SaveChanges();
        }

        public virtual int Update(Entity item)
        {
            _context.Update(item);
            return _context.SaveChanges();
        }

        public virtual int UpdateRange(IEnumerable<Entity> items)
        {
            _context.UpdateRange(items);
            return _context.SaveChanges();
        }

        public virtual int Delete(Key id)
        {
            var toRemove = Find(id);
            _context.Remove(toRemove);
            return _context.SaveChanges();
        }

        public virtual int Count()
        {
            return FindAll().Count();
        }

        public virtual void Dispose()
        {
            _context.Dispose();
        }
    }
}