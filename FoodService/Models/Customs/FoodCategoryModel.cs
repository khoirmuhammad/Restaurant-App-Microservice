using MongoDB.Bson;

namespace FoodService.Models.Customs
{
    public class FoodCategoryModel
    {
        public string Id { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Deleted { get; set; }
    }
}
