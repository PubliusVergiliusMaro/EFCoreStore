namespace EFCoreStore.Database.Entities
{
	public class ProductEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime? DeletedOn { get; set; }
		public int? CheckFK { get; set; }
		public CheckEntity Check { get; set; }
	}
}
