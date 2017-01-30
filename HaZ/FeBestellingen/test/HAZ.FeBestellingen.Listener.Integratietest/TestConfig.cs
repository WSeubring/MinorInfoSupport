using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HAZ.FeBestellingen.Infrastructure.Repositories;

namespace HAZ.FeBestellingen.Listener.Integratietest
{
    public class TestConfig
    { 

        public IConfigurationRoot Configuration { get; }

        public TestConfig()
        {
            var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public DbContextOptions<BestellingenContext> GetDbContext()
        {
            DbContextOptionsBuilder<BestellingenContext> builder = new DbContextOptionsBuilder<BestellingenContext>();
            builder.UseMySql(
                            Configuration.GetConnectionString("DataAccessMySqlProvider"),
                            b => b.MigrationsAssembly("AspNet5MultipleProject"));
            return builder.Options;

        }

    }
}
