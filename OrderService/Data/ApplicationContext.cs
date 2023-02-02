using Microsoft.EntityFrameworkCore;
using OrderService.Models;
using OrderService.Models.Configurations;

namespace OrderService.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusConfiguration());
            modelBuilder.ApplyConfiguration(new FoodViewModelConfiguration());
        }

        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderDetail> OrdersDetails { get; set;} = default!;
        public DbSet<OrderStatus> OrderStatuses { get; set; } = default!;

        public DbSet<FoodViewModel> FoodViewModels { get; set; } = default!;
        public DbSet<CustomerViewModel> CustomerViewModels { get; set; } = default!;
    }
}
