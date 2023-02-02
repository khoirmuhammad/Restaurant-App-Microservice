namespace OrderService.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal BillTotal { get; set; }
        public decimal DiscountByPoint { get; set; }
        public decimal FinalTotal { get; set;}
        public int Status { get; set; }
        public bool Deleted { get; set; }

        public virtual IList<OrderDetail> OrderDetails { get; set; } = default!;
    }
}
