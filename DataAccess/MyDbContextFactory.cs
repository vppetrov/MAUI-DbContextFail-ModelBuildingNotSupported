using DataAccess;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess
{
	internal class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
	{
		public MyDbContext CreateDbContext(string[] args)
		{
			return new MyDbContext("DesignTimeBasePath");
		}
	}
}
