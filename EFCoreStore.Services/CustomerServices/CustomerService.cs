using EFCoreStore.Database.Entities;
using EFCoreStore.Database.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace EFCoreStore.Services.CustomerServices
{
	public class CustomerService : ICustomerService
	{
		private readonly IGenericRepository<CustomerEntity> _customerRepository;
		public CustomerService(IGenericRepository<CustomerEntity> customerRepository)
		{
			_customerRepository = customerRepository;
		}

		public void Create(CustomerEntity customer)
		{
			_customerRepository.Create(customer);
		}

		public bool Delete(int Id)
		{
			CustomerEntity dbRecord = _customerRepository.Table
				.FirstOrDefault(customer => customer.Id == Id);
			if (dbRecord == null)
			{
				return false;
			}
			_customerRepository.Remove(dbRecord);
			return true;
		}
		public bool Update(CustomerEntity customer)
		{
			try
			{
				_customerRepository.Update(customer);
				_customerRepository.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public CustomerEntity GetById(int Id)
		{
			CustomerEntity dbRecord = _customerRepository.Table
				.Where(customer => !customer.DeletedOn.HasValue)
				.Include(customer => customer.Checks)
				.ThenInclude(prod => prod.Products)
				.FirstOrDefault(customer => customer.Id == Id);
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public List<ProductEntity> GetProductsById(int id)
		{
			CustomerEntity dbRecord = _customerRepository.Table
				.Where(customer => !customer.DeletedOn.HasValue)
				.Include(customer => customer.Checks)
				.ThenInclude(prod => prod.Products)
				.FirstOrDefault(customer => customer.Id == id);
			if (dbRecord == null)
			{
				return null;
			}
			List<CheckEntity> checks = dbRecord.Checks;
			List<ProductEntity> products = new List<ProductEntity>();
			foreach (CheckEntity check in checks)
			{
				products.AddRange(check.Products);
			}
			return products;
		}
	}
}
