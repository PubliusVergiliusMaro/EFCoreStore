using EFCoreStore.Database.Configurations;
using EFCoreStore.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreStore.Database
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<CustomerEntity> Customers { get; set; }
		public DbSet<CheckEntity> Checks { get; set; }
		public DbSet<ProductEntity> Products { get; set; }
		public ApplicationDbContext()
		{
			Database.Migrate();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Data Source=localhost;Database=EFCoreStoreDb;Trusted_Connection=True;TrustServerCertificate=True;");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new CustomerConfiguration());
			modelBuilder.ApplyConfiguration(new CheckConfiguration());
			modelBuilder.ApplyConfiguration(new ProductConfiguration());
		}
	}
}
