using EFCoreStore.Database.Entities;
using EFCoreStore.Database.GenericRepository;
using EFCoreStore.Services.CheckServices;
using EFCoreStore.Services.CustomerServices;

namespace EFCoreStore.Services.ProductServices
{
	public class ProductService : IProductService
	{
		private readonly IGenericRepository<ProductEntity> _productRepository;
		private readonly IGenericRepository<CustomerEntity> _customerRepository;
		private readonly IGenericRepository<CheckEntity> _checkRepository;

		public ProductService(IGenericRepository<ProductEntity> productRepository, IGenericRepository<CustomerEntity> customerRepository, IGenericRepository<CheckEntity> checkRepository)
		{
			_productRepository = productRepository;
			_customerRepository = customerRepository;
			_checkRepository = checkRepository;
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
				.Where((prod => !prod.DeletedOn.HasValue && prod.CheckFK == null)) 			
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
				.Where(prod => !prod.DeletedOn.HasValue &&  prod.CheckFK.HasValue)
				
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
		public void Purchase(int CustomerId, List<ProductEntity> productEntities)
		{
			
			CustomerEntity customer = _customerRepository.Table
				.Where(cust => !cust.DeletedOn.HasValue)
				.FirstOrDefault(cust => cust.Id == CustomerId);
			if (customer != null) { 
			  CheckEntity check = new CheckEntity()
			{ Products = productEntities, CreatedOn = DateTime.Now, CustomerFK = customer.Id };
			foreach (ProductEntity productEntity in productEntities)
			{
				productEntity.CheckFK = check.Id;
			}
			_checkRepository.Create(check);
			}
			else Console.WriteLine("Error");
		}
	}
}
