using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IFoodViewModelRepository
    {
        Task<IEnumerable<FoodViewModel>> GetAllAsync();

        IEnumerable<FoodViewModel> GetAll();
        Task<FoodViewModel?> GetByIdAsync(string code);
        FoodViewModel? GetById(string code);

        void Save(FoodViewModel food);
        Task SaveAsync(FoodViewModel food);
    }
}
