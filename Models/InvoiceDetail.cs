namespace SchadTest.Models
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public Int64 InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public Int64 ServiceId { get; set; }
        public string? Description { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public string? PriceDescription { get; set; }
        public decimal TotalItbis { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public virtual Invoice Customer { get; set; } = null!;
    }
}
