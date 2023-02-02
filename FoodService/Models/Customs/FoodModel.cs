using MongoDB.Bson;

namespace FoodService.Models.Customs
{
    public class FoodModel
    {
        public string Id { get; set; } = string.Empty;
        public string CategoryCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public IFormFile Image { get; set; } = default!;
        public string ImageString { get; set; } = string.Empty;
        public bool Deleted { get; set; }
    }
}
