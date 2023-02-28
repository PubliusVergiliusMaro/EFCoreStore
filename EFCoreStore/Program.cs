using EFCoreStore.Database;
using EFCoreStore.Database.Entities;
using EFCoreStore.Database.GenericRepository;
using EFCoreStore.Services.CheckServices;
using EFCoreStore.Services.CustomerServices;
using EFCoreStore.Services.ProductServices;

namespace EFCoreStore
{
	public class Program
	{
		static ApplicationDbContext dbContext;
		static IGenericRepository<ProductEntity> productRepository;
		static IGenericRepository<CustomerEntity> customerRepository;
		static IGenericRepository<CheckEntity> checkRepository;
		static ICheckService checkService;
		static ICustomerService customerService;
		static IProductService productService;
		static void Main(string[] args)
		{
			dbContext = new ApplicationDbContext();
			productRepository = new GenericRepository<ProductEntity>(dbContext);
			customerRepository = new GenericRepository<CustomerEntity>(dbContext);
			checkRepository = new GenericRepository<CheckEntity>(dbContext);

			customerService = new CustomerService(customerRepository);
			productService = new ProductService(productRepository);
			checkService = new CheckService(checkRepository);

			DisplayAllPurchasedProducts();

			DisplayProductsWithCustomerData(2);

			DisplayProductsInCheck(1);

			DisplayProductsWithCustomerData(2);

			DisplayAllNotPurchasedProducts();

			DisplayEachCheck(2);
			Console.WriteLine("Successfully");
			Console.ReadKey();
		}
		public static void Purchase(int CustomerId, List<ProductEntity> productEntities)
		{
			CustomerEntity customer = customerService.GetById(CustomerId);
			CheckEntity check = new CheckEntity()
			{ Products = productEntities, CreatedOn = DateTime.Now, CustomerFK = customer.Id };
			foreach (ProductEntity productEntity in productEntities)
			{
				productEntity.CheckFK = check.Id;
			}
			checkService.Create(check);
		}
		public static void DisplayProductsInCheck(int CheckId)
		{
			CheckEntity check = checkService.GetById(CheckId);
			if (check != null)
			{
				foreach (ProductEntity product in check.Products)
				{
					Console.WriteLine(product.Name);
				}
			}
			else Console.WriteLine("Error");
		}
		public static void DisplayAllPurchasedProducts()
		{
			List<ProductEntity> products = productService.GetAllPurchase();
			if (products != null)
			{
				foreach (ProductEntity product in products)
				{
					Console.WriteLine(product.Name);
				}
			}
		}
		public static void DisplayProductsWithCustomerData(int Id)
		{
			CustomerEntity customerEntity = customerService.GetById(Id);
			if (customerEntity != null)
			{
				Console.WriteLine($"Name - {customerEntity.Name}");
				List<ProductEntity> products = customerService.GetProductsById(Id);
				foreach (ProductEntity product in products)
				{
					Console.WriteLine(product.Name);
				}
			}
			else Console.WriteLine("Error");
		}
		public static void DisplayAllNotPurchasedProducts()
		{
			List<ProductEntity> products = productService.GetAllAvaliable();
			if (products != null)
			{
				foreach (ProductEntity product in products)
				{
					Console.WriteLine(product.Name);
				}
			}
			else Console.WriteLine("Error");
		}
		public static void DisplayEachCheck(int CustomerId)
		{
			CustomerEntity customerEntity = customerService.GetById(CustomerId);
			if (customerEntity != null)
			{
				Console.WriteLine($"Name - {customerEntity.Name}");
				List<CheckEntity> checks = customerEntity.Checks;
				foreach (CheckEntity check in checks)
				{
					Console.WriteLine(check.Id + " - Id");
					foreach (ProductEntity product in check.Products)
					{
						Console.WriteLine(product.Name);
					}
				}
			}
			else Console.WriteLine("Error");
		}
	}
}