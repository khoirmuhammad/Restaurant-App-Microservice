using FoodService.Data;
using FoodService.Models;
using MongoDB.Driver;

namespace FoodService.Repositories
{
    public class FoodCategoryRepository : IFoodCategoryRepository
    {
        private readonly IMongoCollection<FoodCategory> _foodCategories;
        public FoodCategoryRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _foodCategories = database.GetCollection<FoodCategory>(settings.FoodCategoriesCollectionName);
        }

        public async Task Delete(string id)
        {
            await _foodCategories.DeleteOneAsync(d => d.Id == id);
        }

        public async Task<FoodCategory> Get(string id)
        {
            return await _foodCategories.Find(f => f.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FoodCategory>> GetAll()
        {
            return await _foodCategories.Find(FilterDefinition<FoodCategory>.Empty).ToListAsync();
        }

        public async Task Save(FoodCategory category)
        {
            var data = await this.Get(category.Id);

            if (data == null)
            {
                await _foodCategories.InsertOneAsync(category);
            }
            else
            {
                await _foodCategories.ReplaceOneAsync(r => r.Id.Equals(category.Id), category);
            }
        }

        public async Task SoftDelete(string id)
        {
            var data = await Get(id);

            if (data != null)
            {
                data.Deleted = true;
                await _foodCategories.ReplaceOneAsync(r => r.Id == id, data);
            }
        }
    }
}
