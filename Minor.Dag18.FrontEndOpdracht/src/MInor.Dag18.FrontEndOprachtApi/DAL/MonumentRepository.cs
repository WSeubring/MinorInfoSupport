using Enities;
using Minor.Dag19.DAL;
using System;

public class MonumentRepository : IDisposable
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
}