using Microsoft.EntityFrameworkCore;
using Minor.Case1.AdministratieCursusenCursisten.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Case1.AdministratieCursusenCursisten.Agents
{
    public class OverzichtContext : DbContext
    {
        protected virtual DbSet<CursusOverzicht> CursusOverzichts{ get; set; }
    }
}
