using FoodService.Data;
using FoodService.Models;
using MongoDB.Driver;

namespace FoodService.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly IMongoCollection<Food> _foods;
        public FoodRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _foods = database.GetCollection<Food>(settings.FoodsCollectionName);
        }

        public async Task Delete(string id)
        {
            await _foods.DeleteOneAsync(d => d.Id == id);
        }

        public async Task<Food> Get(string id)
        {
            return await _foods.Find(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Food>> GetAll()
        {
            return await _foods.Find(FilterDefinition<Food>.Empty).ToListAsync();
        }

        public async Task<IEnumerable<Food>> GetByCategory(string catgoryCode)
        {
            return await _foods.Find(f => f.CategoryCode == catgoryCode).ToListAsync();
        }

        public async Task Save(Food food)
        {
            var data = await Get(food.Id);

            if (data == null)
            {
                await _foods.InsertOneAsync(food);
            }
            else
            {
                await _foods.ReplaceOneAsync(r => r.Id == food.Id, food);
            }
        }

        public async Task SoftDelete(string id)
        {
            var data = await Get(id);

            if (data != null)
            {
                data.Deleted = true;
                await _foods.ReplaceOneAsync(r => r.Id == id, data);
            }
        }
    }
}
