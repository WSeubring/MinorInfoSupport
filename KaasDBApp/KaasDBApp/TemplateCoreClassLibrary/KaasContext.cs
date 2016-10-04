using System;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;

public class KaasContext : DbContext
{
    public KaasContext()
    {
        Database.EnsureCreated();
    }
    public KaasContext(DbContextOptions<KaasContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;");
        }
    }



    public virtual DbSet<Kaas> Kazen { get; set; }

    public IEnumerable<Kaas> FindAll()
    {
        var kazen = from kaas in Kazen
                    select kaas;
        return kazen.ToList();
     
    }
    public Kaas FindById(int id)
    {
        var kaasQuery = Kazen.Where(k => k.Id == id);

        return kaasQuery.FirstOrDefault();
    }
}