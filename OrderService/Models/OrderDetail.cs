namespace OrderService.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public string FoodItem { get; set; } = string.Empty;
        public uint Qty { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ItemDiscount { get; set; }
        public decimal FinalPrice { get; set; }
        public bool Deleted { get; set; }

        public Order Order { get; set; } = default!;
    }
}
