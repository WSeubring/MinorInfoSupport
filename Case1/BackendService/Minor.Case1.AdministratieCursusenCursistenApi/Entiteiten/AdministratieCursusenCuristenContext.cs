using Microsoft.EntityFrameworkCore;

namespace Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten
{
    public class AdministratieCursusenCuristenContext : DbContext
    {
        public AdministratieCursusenCuristenContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<CursusInstantie> CursusInstanties{ get; set; }

    }
}