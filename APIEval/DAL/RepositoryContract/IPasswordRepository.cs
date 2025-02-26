using DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.RepositoryContract
{
	/// <summary>
	/// Data Access Layer Interface for passwords
	/// </summary>
	public interface IPasswordRepository
	{
		/// <summary>
		/// Retrieve all passwords
		/// </summary>
		/// <returns></returns>
		DbSet<Password> GetAll();

		/// <summary>
		/// Create a new password
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		Task<Password> Create(Password password);

		/// <summary>
		/// Delete a password by its id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<int?> DeleteById(int id);
	}
}
