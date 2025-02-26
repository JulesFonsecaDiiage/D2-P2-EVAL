using DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.RepositoryContract
{
	/// <summary>
	/// Data Access Layer Interface for applications
	/// </summary>
	public interface IApplicationRepository
	{
		/// <summary>
		/// Retrieve all applications
		/// </summary>
		/// <returns></returns>
		DbSet<Application> GetAll();

		Task<Application?> GetById(int id);

		/// <summary>
		/// Create a new application
		/// </summary>
		/// <param name="application"></param>
		/// <returns></returns>
		Task<Application> Create(Application application);
	}
}
