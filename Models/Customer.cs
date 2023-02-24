using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SchadTest.Models
{
    public class Customer
    {
        public Customer()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        [Display(Name = "Customer Name")]
        public string CustName { get; set; } = null!;
        [Display(Name = "Address")]
        public string Adress { get; set; } = null!;
        public bool? Status { get; set; }
        [Display(Name = "Type")]
        public int CustomerTypeId { get; set; }
        [Display(Name = "Type")]
        public virtual CustomerType CustomerType { get; set; } = null!;
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
