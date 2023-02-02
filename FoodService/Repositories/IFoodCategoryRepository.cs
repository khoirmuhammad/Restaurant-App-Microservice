using FoodService.Models;
using System.Collections;

namespace FoodService.Repositories
{
    public interface IFoodCategoryRepository
    {
        Task<IEnumerable<FoodCategory>> GetAll();
        Task<FoodCategory> Get(string id);
        Task Save(FoodCategory category);
        Task Delete(string id);
        Task SoftDelete(string id);

    }
}
