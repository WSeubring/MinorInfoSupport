using System;
using System.Linq;
using Lapiwe.OnderhoudService.Domain;
using Lapiwe.OnderhoudService.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class OnderhoudRepository : IRepository
{
    private DbContextOptions<OnderhoudContext> options;

    public OnderhoudRepository(DbContextOptions<OnderhoudContext> options)
    {
        this.options = options;
    }

    public OnderhoudsOpdracht Find(Guid guid)
    {
        using(var context = new OnderhoudContext(options))
        {
            return context.OnderhoudsOpdrachten.Single(o => o.Guid == guid);
        }
    }

    public void Insert(OnderhoudsOpdracht onderhoudsOpdracht)
    {
        using(var context = new OnderhoudContext(options))
        {
            context.OnderhoudsOpdrachten.Add(onderhoudsOpdracht);
            context.SaveChanges();
        }
    }

    public void Update(OnderhoudsOpdracht onderhoudsOpdracht)
    {
        using (var context = new OnderhoudContext(options))
        {
            context.OnderhoudsOpdrachten.Update(onderhoudsOpdracht);
            context.SaveChanges();
        }
    }
}