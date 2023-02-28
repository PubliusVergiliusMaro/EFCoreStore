using EFCoreStore.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreStore.Database.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
	{
		public void Configure(EntityTypeBuilder<ProductEntity> builder)
		{
			builder
				.ToTable("Products")
				.HasKey(product => product.Id);
			builder
				.HasOne<CheckEntity>(product => product.Check)
				.WithMany(check => check.Products)
				.HasForeignKey(product => product.CheckFK)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}
