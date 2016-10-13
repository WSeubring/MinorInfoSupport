using Microsoft.AspNetCore.Http;
using Minor.Case1.AdministratieCursusenCursisten.Agents.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Case1.AdministratieCursusenCursisten.ViewModels
{
    public class ImporterenViewModel
    {
        [Required]
        public IFormFile File { get; set; }

        public AddFromFileResultReport Report { get; set; }
    }
}
