using EFCoreStore.Database.Entities;
using EFCoreStore.Services.CheckServices;
using EFCoreStore.Services.CustomerServices;

namespace EFCoreStore.Services.ProductServices
{
	public interface IProductService
	{
		void Create(ProductEntity product);

		bool Update(ProductEntity product);

		bool Delete(int Id);

		ProductEntity GetById(int Id);

		List<ProductEntity> GetAll();

		List<ProductEntity> GetAllPurchase();

		List<ProductEntity> GetAllAvaliable();

		void Purchase(int CustomerId, List<ProductEntity> productEntities);
		
	}
}
