namespace OrderService.Models.Customs
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal BillTotal { get; set; }
        public decimal DiscountByPoint { get; set; }
        public decimal FinalTotal { get; set; }
        public int Status { get; set; }

        public IList<OrderDetailModel> Details { get; set; } = default!;
    }
}
