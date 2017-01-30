using HAZ.FeWebshop.Infrastructure.Repositories;
using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HAZ.FeWebshop.Listener
{
    public class StartupHelper
    {

        public static DbContextOptions<WebshopContext> GetDbContext(IConfigurationRoot configuration)
        {
            DbContextOptionsBuilder<WebshopContext> builder = new DbContextOptionsBuilder<WebshopContext>();
            var sqlConnectionString = configuration.GetConnectionString("DataAccessMySqlProvider") ?? "server=localhost;port=33306;userid=eventlogs;password=eventlogs;database=eventlogs;";
            builder.UseMySql(sqlConnectionString, b => b.MigrationsAssembly("AspNet5MultipleProject"));
            return builder.Options;
        }

        public static void WaitForMysql(ILogger<Program> logger, IConfigurationRoot configuration)
        {
            bool online = false;
            logger.LogInformation("Waiting for mysql...");

            int tryLimit = 120;
            int tryCount = 0;
            while (!online)
            {
                try
                {
                    logger.LogInformation("Polling mysql...");
                    using (var artikelRepo = new ArtikelRepository(new WebshopContext(GetDbContext(configuration))))
                    {
                        logger.LogInformation("mysql is online!");
                        online = true;
                    }
                }
                catch (Exception e)
                {

                    logger.LogWarning("mysql is still offline, retry in 1s");
                    Thread.Sleep(1000);
                    tryCount++;
                    if (tryCount >= tryLimit)
                    {
                        logger.LogCritical("mysql is still offline after 120s, stop polling ");
                        break;
                    }
                }
            }
        }

        public static void WaitForRabbitMQ(ILogger<Program> logger)
        {
            bool online = false;
            logger.LogInformation("Waiting for rabbit...");

            int tryLimit = 120;
            int tryCount = 0;
            while (!online)
            {
                try
                {
                    logger.LogInformation("Polling rabbit...");
                    using (var publisher = new EventPublisher(BusOptions.CreateFromEnvironment()))
                    {
                        logger.LogInformation("rabbit is online!");
                        online = true;
                    }
                }
                catch (Exception e)
                {
                    logger.LogWarning("rabbit is still offline, retry in 1s");
                    Thread.Sleep(1000);
                    tryCount++;
                    if (tryCount >= tryLimit)
                    {
                        logger.LogCritical("rabbit is still offline after 120s, stop polling ");
                        break;
                    }
                }
            }
        }

    }
}
