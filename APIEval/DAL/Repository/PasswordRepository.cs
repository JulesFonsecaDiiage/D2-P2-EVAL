

using DAL.Entity;
using DAL.RepositoryContract;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
	public class PasswordRepository(AppDbContext context) : IPasswordRepository
	{
		private readonly AppDbContext context = context;

		/// <inheritdoc/>
		public DbSet<Password> GetAll()
		{
			return this.context.Password;
		}

		/// <inheritdoc/>
		public async Task<Password> Create(Password password)
		{
			context.Password.Add(password);
			await context.SaveChangesAsync();

			return password;
		}

		/// <inheritdoc/>
		public async Task<int?> DeleteById(int id)
		{
			var password = await context.Password.FindAsync(id);

			if (password == null)
			{
				return null;
			}
			context.Password.Remove(password);

			return await context.SaveChangesAsync();
		}
	}
}
