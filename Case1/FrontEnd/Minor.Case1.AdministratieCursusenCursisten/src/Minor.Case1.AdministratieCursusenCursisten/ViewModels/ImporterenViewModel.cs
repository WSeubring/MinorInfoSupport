using Microsoft.AspNetCore.Http;
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
    }
}
