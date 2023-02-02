using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IFoodViewModelRepository
    {
        Task<IEnumerable<FoodViewModel>> GetAll();
        Task<FoodViewModel?> GetById(string code);
    }
}
