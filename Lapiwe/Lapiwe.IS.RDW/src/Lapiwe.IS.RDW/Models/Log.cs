using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.IS.RDW.Models
{
    public class Log
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "text")]
        [StringLength(4000)]
        public string Xml { get; set; }
        public string Type { get; set; }
    }
}
