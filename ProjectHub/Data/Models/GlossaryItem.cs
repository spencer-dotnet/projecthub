using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHub.Data.Models
{
    public class GlossaryItem
    {
        [Required]
        public string Term { get; set; }
        public string Definition { get; set; }
    }
}
