namespace EFCoreStore.Database.Entities
{
	public class CheckEntity
	{
		public CheckEntity()
		{
			Products = new List<ProductEntity>();
		}
		public int Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime? DeletedOn { get; set;}

		public int? CustomerFK { get; set;}
		public CustomerEntity Customer { get; set;}
		public List<ProductEntity> Products { get; set;}
	}
}
