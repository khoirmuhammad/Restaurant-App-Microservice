using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Models;
using System.Reflection.Emit;

namespace UserService.Models.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.UserId).IsRequired();
            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.Address).IsRequired();
            builder.Property(a => a.Phone).IsRequired();

            builder.HasOne(e => e.User)
                    .WithOne(e => e.Customer)
                    .HasForeignKey<Customer>(e => e.UserId);

            builder.HasQueryFilter(x => !x.Deleted);
        }
    }
}
