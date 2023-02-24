namespace SchadTest.Models
{
    public class CustomerType
    {
        public CustomerType()
        {
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
