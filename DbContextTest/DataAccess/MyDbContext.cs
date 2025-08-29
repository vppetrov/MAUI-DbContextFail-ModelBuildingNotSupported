using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DbContextTest.DataAccess.Entities;
using DbContextTest.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Storage;

namespace DbContextTest.DataAccess
{
	public class MyDbContext : DbContext
	{
		#region Constructor
		protected readonly IMyLogger Logger;

		public MyDbContext(IMyLogger logger)
		{
			Logger = logger;
		}
		#endregion

		public DbSet<MyEntity> MyEntities { get; set; }

		#region OnConfiguring

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			var connectionString = GetConnectionString();
			Logger.Log($"{nameof(OnConfiguring)} for {GetType().Name} with connection string {connectionString}");
			optionsBuilder.UseSqlite(connectionString);
		}

		private string GetConnectionString()
		{
			var di = new DirectoryInfo(Path.Combine(FileSystem.AppDataDirectory, "Databases"));
			if (di.Exists)
			{
				Logger.Log($"Folder exists {di.FullName}");
			}
			else
			{
				Logger.Log($"Creating folder {di.FullName}");
				di.Create();
			}
			var dbName = $"{GetType().Name}.db";
			var fullDbName = Path.Combine(di.FullName, dbName);

			return @$"Data Source=""{fullDbName}""";
		}
		#endregion

		#region RunMigrationsAsync
		public async Task RunMigrationsAsync(CancellationToken cancellationToken)
		{
			Logger.Log($"Async running migrations on {GetType().Name} with connection string {Database.GetConnectionString()}");
			try
			{
				await Database.MigrateAsync(cancellationToken);
				Logger.Log($"Successfully completed async migrations on {GetType().Name} with connection string {Database.GetConnectionString()}");
			}
			catch (Exception ex)
			{
				Logger.Log($"Failed async migrations on {GetType().Name} with connection string {Database.GetConnectionString()}:\r\n{ex}");
			}
		}

		#endregion
	}
}
