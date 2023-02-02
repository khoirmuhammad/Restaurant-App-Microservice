using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Models.Configurations
{
    public class FoodViewModelConfiguration : IEntityTypeConfiguration<FoodViewModel>
    {
        public void Configure(EntityTypeBuilder<FoodViewModel> builder)
        {
            builder.HasKey(a => a.Code);
            builder.Property(a => a.Name).IsRequired();
        }
    }
}
