using DataAccess.Entities;
using DataAccess.MyDbContextCompiled;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
	public class MyDbContext : DbContext
	{
		private readonly string _basePath;
		public DbSet<MyEntity> MyEntities { get; set; }

		public MyDbContext(string basePath)
		{
			this._basePath = basePath;
		}

		static MyDbContext()
		{
			// To avoid the error on iOS Release model:
			// "Model building is not supported when publishing with NativeAOT. Use a compiled model"
			// we need to use compiled models. However, there is a bug in them causing the initialization
			// to freeze on Android as explained here:
			// https://github.com/dotnet/efcore/issues/32346
			// The workaround is to set this switch to true before any db context is created.
			System.AppContext.SetSwitch("Microsoft.EntityFrameworkCore.Issue31751", true);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			// Add compiled model: needed on iOS for Release model. Otherwise, there is an error:
			// "Model building is not supported when publishing with NativeAOT. Use a compiled model"
			optionsBuilder.UseModel(MyDbContextModel.Instance);

			var connectionString = @$"Data Source=""{Path.Combine(this._basePath, $"{GetType().Name}.db")}""";
			optionsBuilder.UseSqlite(connectionString);
		}
	}
}
