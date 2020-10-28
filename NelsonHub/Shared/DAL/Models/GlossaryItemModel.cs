using System.ComponentModel.DataAnnotations;

namespace NelsonHub.Shared.DAL.Models
{
    public class GlossaryItemModel
    {
        [Required]
        public string Term { get; set; }
        public string Definition { get; set; }
    }
}
