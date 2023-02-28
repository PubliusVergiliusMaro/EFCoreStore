namespace EFCoreStore.Database.Entities
{
	public class CustomerEntity
	{
		public CustomerEntity()
		{
			Checks = new List<CheckEntity>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime? DeletedOn { get; set; }
		public List<CheckEntity> Checks { get; set; }
	}
}
