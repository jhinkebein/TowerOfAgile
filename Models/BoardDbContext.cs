using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TowerOfAgile.Models
{
	public class BoardDbContext : DbContext 
	{
		public BoardDbContext(DbContextOptions<BoardDbContext> options)
			: base(options) { }

		public DbSet<Board> Boards { get; set; }
		public DbSet<BoardItem> BoardItems { get; set; }
		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{
		//	modelBuilder.Entity<BoardItem>()
		//		.HasOne(b => b.Board)
		//		.WithMany(i => i.Items)
		//		.OnDelete(DeleteBehavior.Cascade);
		//}
	}
	public class BoardContextFactory : IDesignTimeDbContextFactory<BoardDbContext>
	{
		public BoardDbContext CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var builder = new DbContextOptionsBuilder<BoardDbContext>();

			var connectionString = configuration.GetConnectionString("BoardDbContext");

			builder.UseSqlServer(connectionString);

			return new BoardDbContext(builder.Options);
		}
	}
}
