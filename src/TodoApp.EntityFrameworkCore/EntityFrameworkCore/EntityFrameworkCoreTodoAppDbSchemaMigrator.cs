using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Data;
using Volo.Abp.DependencyInjection;

namespace TodoApp.EntityFrameworkCore
{
    public class EntityFrameworkCoreTodoAppDbSchemaMigrator
        : ITodoAppDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreTodoAppDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the BookStoreDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<TodoAppDbContext>()
                .Database
                .MigrateAsync();
        }

        public async Task MigrateAsync(string con) // written by sai
        {

            var context = _serviceProvider
                  .GetRequiredService<TodoAppDbContext>();
            context.Database.SetConnectionString(con);
            context.Database.Migrate();

        }
    }
}
