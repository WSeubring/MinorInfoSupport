using Lapiwe.Audit.Domain.Contracts;
using Lapiwe.Audit.Infrastructure.Context;
using Lapiwe.Audit.Infrastructure.Repositories;
using Lapiwe.Audit.Listener;
using Lapiwe.Audit.Publisher;
using Lapiwe.EventBus.Domain;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.Entity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.WSA.Audit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = @"server=db;userid=admin;password=1234;database=auditlog;";

            DbContextOptions options = new DbContextOptionsBuilder().UseMySQL(connectionString).Options;
            AuditLogDbContext context = new AuditLogDbContext(options);
            BusOptions busOptions = new BusOptions(hostname:"rabbitmq",port:5672,username:"Lapiwe",password: "Lapiwe123", exchangeName:"Lapiwe.GMS");

            using (IRepository repo = new AuditLogRepository(context))
            using (var all = new AllEventDispatcher(repo, busOptions))
            using (var publisher = new AuditPublisher(busOptions))
            using (var dispatcher = new AuditDispatcher(repo, publisher, busOptions))
            {
                Console.Read();
            }
        }
    }
}
