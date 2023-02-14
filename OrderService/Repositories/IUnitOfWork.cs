namespace OrderService.Repositories
{
    public interface IUnitOfWork
    {
        IOrderRepository OrderRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IFoodViewModelRepository FoodViewModelRepository { get; }
        Task<bool> SaveChangesAsync();
        bool SaveChanges();
    }
}
