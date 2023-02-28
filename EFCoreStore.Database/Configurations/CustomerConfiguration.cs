using EFCoreStore.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreStore.Database.Configurations
{
	public class CustomerConfiguration : IEntityTypeConfiguration<CustomerEntity>
	{
		public void Configure(EntityTypeBuilder<CustomerEntity> builder)
		{
			builder.ToTable("Customers").HasKey(customer => customer.Id);
		}
	}
}
