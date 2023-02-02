using UserService.Models;

namespace UserService.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer?> GetById(int id);
        Task<Customer?> GetByUserId(Guid userId);
        Task Save(Customer customer);
        Task Delete(int id);
        Task SoftDelete(int id);
        Task SoftDelete(Guid userId);
    }
}
