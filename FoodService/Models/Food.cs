using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FoodService.Models
{
    public class Food
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
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
