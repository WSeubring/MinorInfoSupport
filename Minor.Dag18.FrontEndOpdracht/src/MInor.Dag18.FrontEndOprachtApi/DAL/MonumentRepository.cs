using DAL.Interfaces;
using Enities;
using Minor.Dag19.DAL;
using System;
using System.Linq;
using System.Collections.Generic;

public class MonumentRepository : IRepository<Monument, int> , IDisposable
{
    private MonumentContext _context;

    public MonumentRepository(MonumentContext context)
    {
        _context = context;
    }

    public void Add(Monument item)
    {
        _context.Monumenten.Add(item);
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public void Delete(int id)
    {
        var item = _context.Monumenten.Where(m => m.ID == id).FirstOrDefault();
        if (item != null)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Monument> FindAll()
    {
        return _context.Monumenten.ToList();
    }

    public Monument FindByKey(int key)
    {
        return _context.Monumenten.Single(m => m.ID == key);
    }
}