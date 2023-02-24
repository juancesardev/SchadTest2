using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SchadTest.Models
{
    public class Details_Temp
    {
        public Int64 Id { get; set; }
        public string? Header_Guid { get; set; }
        public Int64 Header_Id { get; set; }
        public int ServiceId { get; set; }
        public string? Description { get; set; }
        public int Qty { get; set; }
        [Display(Name = "Price / Without ITBIS")]
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public DateTime? DateAdd { get; set; }
    }
}
