using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Microsoft.Extensions.Configuration;
using IntegrationTest.DAL;
using InfoSupport.WSA.Infrastructure;

namespace IntegrationTest
{
    public class StartupHelper
    {
        public IConfigurationRoot Configuration { get; }

        public StartupHelper()
        {
            var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void WaitForDb(string dbName)
        {
            bool online = false;
            Console.WriteLine("Waiting for " + dbName + ".mysql ...");

            int tryLimit = 120;
            int tryCount = 0;
            while (!online)
            {
                try
                {
                    Console.WriteLine("Polling " + dbName + ".mysql...");
                    DbContext context = null;
                    try
                    {
                        switch (dbName)
                        {
                            case "FeWebshop": context = new FeWebshopContext(GetFeWebshopContext()); break;
                            case "FeBestellingen": context = new FeBestellingenContext(GetFeBestellingenContext()); break;
                            case "PsWinkelen": context = new PsWinkelenContext(GetPsWinkelenContext()); break;
                            case "DsBestellingen": context = new DsBestellingenContext(GetDsBestellingenContext()); break;
                            default: throw new Exception("Unknown db: " + dbName);
                        }
                        Console.WriteLine("" + dbName + ".mysql is online!");
                        online = true;
                    }
                    finally
                    {
                        context?.Dispose();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(dbName + ".mysql is still offline, retry in 1s > " + e.Message);
                    Thread.Sleep(1000);
                    tryCount++;
                    if (tryCount >= tryLimit)
                    {
                        Console.WriteLine("" + dbName + ".mysql is still offline after 120s, stop polling ");
                        break;
                    }
                }
            }
        }

        public DbContextOptions<FeWebshopContext> GetFeWebshopContext()
        {
            DbContextOptionsBuilder<FeWebshopContext> builder = new DbContextOptionsBuilder<FeWebshopContext>();
            builder.UseMySql(
                            Configuration.GetConnectionString("FeWebshop"),
                            b => b.MigrationsAssembly("AspNet5MultipleProject"));
            return builder.Options;
        }

        public DbContextOptions<FeBestellingenContext> GetFeBestellingenContext()
        {
            DbContextOptionsBuilder<FeBestellingenContext> builder = new DbContextOptionsBuilder<FeBestellingenContext>();
            builder.UseMySql(
                            Configuration.GetConnectionString("FeBestellingen"),
                            b => b.MigrationsAssembly("AspNet5MultipleProject"));
            return builder.Options;
        }

        public DbContextOptions<PsWinkelenContext> GetPsWinkelenContext()
        {
            DbContextOptionsBuilder<PsWinkelenContext> builder = new DbContextOptionsBuilder<PsWinkelenContext>();
            builder.UseMySql(
                            Configuration.GetConnectionString("PsWinkelen"),
                            b => b.MigrationsAssembly("AspNet5MultipleProject"));
            return builder.Options;
        }

        public DbContextOptions<DsBestellingenContext> GetDsBestellingenContext()
        {
            DbContextOptionsBuilder<DsBestellingenContext> builder = new DbContextOptionsBuilder<DsBestellingenContext>();
            builder.UseMySql(
                            Configuration.GetConnectionString("DsBestellingen"),
                            b => b.MigrationsAssembly("AspNet5MultipleProject"));
            return builder.Options;
        }

        public void WaitForRabbitMQ()
        {
            bool online = false;
            Console.WriteLine("Waiting for rabbit...");

            int tryLimit = 120;
            int tryCount = 0;
            while (!online)
            {
                try
                {
                    Console.WriteLine("Polling rabbit...");
                    using (var publisher = new EventPublisher(BusOptions.CreateFromEnvironment()))
                    {
                        Console.WriteLine("Hrabbit is online!");
                        online = true;
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine("rabbit is still offline, retry in 1s");
                    Thread.Sleep(1000);
                    tryCount++;
                    if (tryCount >= tryLimit)
                    {
                        Console.WriteLine("rabbit is still offline after 120s, stop polling ");
                        break;
                    }
                }
            }
        }


    }
}
