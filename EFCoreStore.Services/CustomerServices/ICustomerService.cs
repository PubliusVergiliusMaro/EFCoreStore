using EFCoreStore.Database.Entities;

namespace EFCoreStore.Services.CustomerServices
{
	public interface ICustomerService
	{
		void Create(CustomerEntity customer);

		bool Delete(int Id);

		bool Update(CustomerEntity customer);

		List<ProductEntity> GetProductsById(int id);

		CustomerEntity GetById(int Id);
	}
}
