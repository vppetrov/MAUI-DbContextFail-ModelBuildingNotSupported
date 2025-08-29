using System.IO;
using DbContextTest.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Storage;

namespace DbContextTest.DataAccess
{
	public class MyDbContext : DbContext
	{
		public DbSet<MyEntity> MyEntities { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			var connectionString = @$"Data Source=""{Path.Combine(FileSystem.AppDataDirectory, $"{GetType().Name}.db")}""";
			optionsBuilder.UseSqlite(connectionString);
		}
	}
}
