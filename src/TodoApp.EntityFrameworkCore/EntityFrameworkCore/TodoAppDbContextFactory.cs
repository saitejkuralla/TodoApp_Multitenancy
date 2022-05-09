using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;

namespace TodoApp.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
    * (like Add-Migration and Update-Database commands) */
    public class TodoAppDbContextFactory : IDesignTimeDbContextFactory<TodoAppDbContext>
    {
        private readonly ICurrentTenant _currentTenant;//sai
        private readonly IConnectionStringResolver _connectionStringResolver;  //sai
        public TodoAppDbContextFactory(ICurrentTenant currentTenant, IConnectionStringResolver connectionStringResolver)
        {
            _currentTenant = currentTenant;// added by sai
            _connectionStringResolver = connectionStringResolver;//sai
        }

        public TodoAppDbContext CreateDbContext(string[] args)
        {
            TodoAppEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<TodoAppDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new TodoAppDbContext(builder.Options,_currentTenant,_connectionStringResolver);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../TodoApp.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
