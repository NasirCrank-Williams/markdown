using markdown.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace markdown.Server.Data
{
	public class UserMarkdownDbContext : DbContext
	{
		public UserMarkdownDbContext(DbContextOptions<UserMarkdownDbContext> options) : base(options)
		{

		}

		public DbSet<User> Users { get; set; }
		public DbSet<Markdown> Markdowns { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			Guid userId = Guid.NewGuid();

			modelBuilder.Entity<Markdown>().HasData(new
			{
				MarkdownId = Guid.NewGuid(),
				Content = "# Hey, How are you doing?",
				DateTime = DateTime.Now,
				UserId = userId,
			});

			modelBuilder.Entity<User>().HasData(new
			{
				UserId = userId,
				UserName = "nassdaman",
				Email = "email@email.com",
				Password = "password",
				Markdowns = new List<Markdown>() {}
			});
		}
	}
}
