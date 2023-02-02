using Microsoft.EntityFrameworkCore;
using UserService.Models;
using UserService.Models.Configurations;

namespace UserService.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
    }
}
