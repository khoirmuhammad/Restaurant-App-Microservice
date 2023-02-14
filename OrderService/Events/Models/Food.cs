using System;

namespace OrderService.Events.Models
{
    public class Food
    {
        public string Id { get; set; } = string.Empty;
        public string CategoryCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public uint MinQtyDiscount { get; set; }
        public byte[] Image { get; set; } = default!;
        public bool Deleted { get; set; }

    }
}
