using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHub.Data.Models
{
    public enum ReferenceCategory
    {
        Programming,
        C_Sharp,
        Blazor,
        RaspberryPi,
        Other,
        DotNetCore

    }
    public class Reference
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public ReferenceCategory Category { get; set; }
    }
}
