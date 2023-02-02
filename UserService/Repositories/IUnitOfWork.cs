namespace UserService.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        Task<bool> SaveAsync();
    }
}
