using Lapiwe.OnderhoudService.Domain;
using Microsoft.EntityFrameworkCore;

public class OnderhoudContext : DbContext
{
    public DbSet<OnderhoudsOpdracht> OnderhoudsOpdrachten { get; set; }

    public OnderhoudContext(DbContextOptions<OnderhoudContext> options) : base(options)
    {
        Database.EnsureCreated();
    }


}