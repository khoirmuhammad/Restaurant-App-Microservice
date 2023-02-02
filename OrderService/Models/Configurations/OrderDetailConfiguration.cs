using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Models.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(ad => ad.Id);
            builder.Property(ad => ad.Id).UseIdentityColumn();
            builder.Property(ad => ad.OrderId).IsRequired();
            builder.Property(ad => ad.FoodItem).IsRequired();

            builder.HasOne<Order>(ad => ad.Order)
                .WithMany(a => a.OrderDetails)
                .HasForeignKey(ad => ad.OrderId);

            builder.HasQueryFilter(x => !x.Deleted);
        }
    }
}
