using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FoodService.Models
{
    public class FoodCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Deleted { get; set; }
    }
}
