namespace OrderService.Models.Customs
{
    public class OrderDetailModel
    {
        public int Id { get; set; }
        public string FoodItem { get; set; } = string.Empty;
        public uint Qty { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ItemDiscount { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
