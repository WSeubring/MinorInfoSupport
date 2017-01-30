using HAZ.FeWebshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HAZ.FeWebshop.Infrastructure.Repositories
{
    public class WebshopContext : DbContext
    {

        public virtual DbSet<Artikel> Artikelen { get; set; }

        public WebshopContext(DbContextOptions<WebshopContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
