using Microsoft.EntityFrameworkCore;
using SchadTest.Models;

namespace SchadTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<CustomerType> CustomerTypes { get; set; } = null!;
        public virtual DbSet<Invoice> Invoice { get; set; } = null!;
        public virtual DbSet<InvoiceDetail> InvoiceDetail { get; set; } = null!;
        public DbSet<Services> Services { get; set; } = null!;
        public DbSet<Header_Temp> HeaderTemp { get; set; } = null!;
        public DbSet<Details_Temp> DetailsTemp { get; set; } = null!;
    }
}
