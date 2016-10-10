using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MonumentenGeneratedPageAndAgent.Models
{
    public class MonumentenGeneratedPageAndAgentContext : DbContext
    {
        public MonumentenGeneratedPageAndAgentContext (DbContextOptions<MonumentenGeneratedPageAndAgentContext> options)
            : base(options)
        {
        }

        public DbSet<Monument> Monument { get; set; }
    }
}
