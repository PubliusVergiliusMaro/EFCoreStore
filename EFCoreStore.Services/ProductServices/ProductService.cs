using EFCoreStore.Database.Entities;
using EFCoreStore.Database.GenericRepository;
namespace EFCoreStore.Services.ProductServices
{
	public class ProductService : IProductService
	{
		private readonly IGenericRepository<ProductEntity> _productRepository;
		public ProductService(IGenericRepository<ProductEntity> productRepository)
		{
			_productRepository = productRepository;
		}
		public void Create(ProductEntity product)
		{
			_productRepository.Create(product);

		}
		public bool Delete(int Id)
		{
			ProductEntity dbRecord = _productRepository.Table
				.FirstOrDefault(prod => prod.Id == Id);
			if (dbRecord == null)
			{
				return false;
			}
			_productRepository.Remove(dbRecord);

			return true;
		}
		public bool Update(ProductEntity product)
		{
			try
			{
				_productRepository.Update(product);
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public List<ProductEntity> GetAll()
		{
			List<ProductEntity> dbRecord = _productRepository.Table
				.Where(prod => !prod.DeletedOn.HasValue)
				.ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public List<ProductEntity> GetAllAvaliable()
		{
			List<ProductEntity> dbRecord = _productRepository.Table
				.Where(prod => !prod.DeletedOn.HasValue)
				.Where(prod => prod.CheckFK == null)
				.ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public List<ProductEntity> GetAllPurchase()
		{
			List<ProductEntity> dbRecord = _productRepository.Table
				.Where(prod => !prod.DeletedOn.HasValue)
				.Where(prod => prod.CheckFK.HasValue)
				.ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public ProductEntity GetById(int Id)
		{
			ProductEntity dbRecord = _productRepository.Table
				.Where(prod => !prod.DeletedOn.HasValue)
				.FirstOrDefault(prod => prod.Id == Id);
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
	}
}
