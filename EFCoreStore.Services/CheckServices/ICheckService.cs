using EFCoreStore.Database.Entities;

namespace EFCoreStore.Services.CheckServices
{
	public interface ICheckService
	{
		void Create(CheckEntity check);

		bool Delete(int Id);

		bool Update(CheckEntity check);

		CheckEntity GetById(int Id);
	}
}
