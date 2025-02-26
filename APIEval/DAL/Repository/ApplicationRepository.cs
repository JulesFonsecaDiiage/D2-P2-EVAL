using DAL.Entity;
using DAL.RepositoryContract;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
	public class ApplicationRepository(AppDbContext context) : IApplicationRepository
	{
		private readonly AppDbContext context = context;

		/// <inheritdoc/>
		public DbSet<Application> GetAll()
		{
			return this.context.Application;
		}

		/// <inheritdoc/>
		public async Task<Application?> GetById(int id)
		{
			return await context.Application.FindAsync(id);
		}

		/// <inheritdoc/>
		public async Task<Application> Create(Application application)
		{
			context.Application.Add(application);
			await context.SaveChangesAsync();

			return application;
		}
	}
}
