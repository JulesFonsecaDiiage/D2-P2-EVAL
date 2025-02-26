using Microsoft.EntityFrameworkCore;
using DAL.Entity;

namespace DAL
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options) { }

		public DbSet<Application> Application { get; set; }
		public DbSet<Password> Password { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Relations
			modelBuilder.Entity<Application>()
				.HasMany(a => a.Passwords)
				.WithOne(p => p.Application)
				.HasForeignKey(p => p.ApplicationId);
		}
	}
}
