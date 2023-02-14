using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Repositories
{
    public class FoodViewModelRepository : IFoodViewModelRepository
    {
        private readonly ApplicationContext _context;
        public FoodViewModelRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FoodViewModel>> GetAllAsync()
        {
            return await _context.FoodViewModels.AsNoTracking().ToListAsync();
        }

        public IEnumerable<FoodViewModel> GetAll()
        {
            return _context.FoodViewModels.AsNoTracking().ToList();
        }

        public async Task<FoodViewModel?> GetByIdAsync(string code)
        {
            return await _context.FoodViewModels.FirstOrDefaultAsync(x => x.Code == code);
        }

        public FoodViewModel? GetById(string code)
        {
            return _context.FoodViewModels.FirstOrDefault(x => x.Code == code);
        }

        public void Save(FoodViewModel food)
        {
            var data = GetById(food.Code);

            if (data == null)
            {
                _context.FoodViewModels.Add(food);
            }
            else
            {
                _context.FoodViewModels.Update(data);
            }
        }

        public async Task SaveAsync(FoodViewModel food)
        {
            var data = await GetByIdAsync(food.Code);

            if (data == null)
            {
                await _context.FoodViewModels.AddAsync(food);
            }
            else
            {
                _context.FoodViewModels.Update(data);
            }
        }
    }
}
