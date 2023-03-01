using DAL;
using Microsoft.EntityFrameworkCore;

namespace WebExplorer.Extensions
{
    public static class MigrateDatabaseClass
    {
        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            try
            {
                scope.ServiceProvider.GetService<ExplorerContext>()?.Database.Migrate();
            }
            catch
            {
                throw;
            }
            return app;
        }
    }
}
