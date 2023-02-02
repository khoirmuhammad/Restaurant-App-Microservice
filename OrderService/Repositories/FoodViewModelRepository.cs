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

        public async Task<IEnumerable<FoodViewModel>> GetAll()
        {
            return await _context.FoodViewModels.AsNoTracking().ToListAsync();
        }

        public async Task<FoodViewModel?> GetById(string code)
        {
            return await _context.FoodViewModels.FirstOrDefaultAsync(x => x.Code == code);
        }
    }
}
