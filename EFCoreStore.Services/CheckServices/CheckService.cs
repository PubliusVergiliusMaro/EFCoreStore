using EFCoreStore.Database.Entities;
using EFCoreStore.Database.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace EFCoreStore.Services.CheckServices
{
	public class CheckService : ICheckService
	{
		private readonly IGenericRepository<CheckEntity> _checkRepository;
		public CheckService(IGenericRepository<CheckEntity> checkRepository)
		{
			_checkRepository = checkRepository;
		}
		public void Create(CheckEntity check)
		{
			_checkRepository.Create(check);
		}

		public bool Delete(int Id)
		{
			CheckEntity dbRecord = _checkRepository.Table
				.FirstOrDefault(x => x.Id == Id);
			if (dbRecord == null)
			{
				return false;
			}
			_checkRepository.Remove(dbRecord);
			return true;
		}

		public CheckEntity GetById(int Id)
		{
			CheckEntity dbRecord = _checkRepository.Table
				.Include(check => check.Products)
				.Where(check => !check.DeletedOn.HasValue)
				.FirstOrDefault(check => check.Id == Id);
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public bool Update(CheckEntity check)
		{
			try
			{
				_checkRepository.Update(check);
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
