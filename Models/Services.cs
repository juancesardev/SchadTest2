using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SchadTest.Models
{
    public class Services
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Price / Without ITBIS")]
        public decimal Price { get; set; }
        [Display(Name = "Price Description")]
        public string? PriceDescription { get; set; }
        public bool Status { get; set; } = true;
        public DateTime? DateAdd { get; set; }
    }
}
