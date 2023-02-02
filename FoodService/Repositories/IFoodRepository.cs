using FoodService.Models;

namespace FoodService.Repositories
{
    public interface IFoodRepository
    {
        Task<IEnumerable<Food>> GetAll();
        Task<IEnumerable<Food>> GetByCategory(string catgoryCode);
        Task<Food> Get(string id);
        Task Save(Food food);
        Task Delete(string id);
        Task SoftDelete(string id);
    }
}
