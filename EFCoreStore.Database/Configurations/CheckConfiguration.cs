using EFCoreStore.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EFCoreStore.Database.Configurations
{
	public class CheckConfiguration : IEntityTypeConfiguration<CheckEntity>
	{
		public void Configure(EntityTypeBuilder<CheckEntity> builder)
		{
			builder
				.ToTable("Checks")
				.HasKey(check => check.Id);
			builder.HasOne<CustomerEntity>(check=>check.Customer)
				.WithMany(customer=>customer.Checks)
				.HasForeignKey(check=>check.CustomerFK)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
