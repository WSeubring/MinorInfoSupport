using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HAZ.FeWebshop.Infrastructure.Repositories;

namespace HAZ.FeWebshop.Listener.Test
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

        public DbContextOptions<WebshopContext> GetDbContext()
        {
            DbContextOptionsBuilder<WebshopContext> builder = new DbContextOptionsBuilder<WebshopContext>();
            builder.UseMySql(
                            Configuration.GetConnectionString("DataAccessMySqlProvider"),
                            b => b.MigrationsAssembly("AspNet5MultipleProject"));
            return builder.Options;

        }

    }
}
