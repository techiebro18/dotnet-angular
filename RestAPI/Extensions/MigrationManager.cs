using FluentMigrator.Runner;
using RestAPI.DataContext;

namespace RestAPI.Extensions
{
    public static class MigrationManager
    {
        public static WebApplication MigrateDatabase(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<DapperDatabase>();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                try
                {
                    //var connection = databaseService.CreateDatabase("dappermigrationexample");
                    migrationService.ListMigrations();
                    migrationService.MigrateUp();
                }
                catch (Exception ex)
                {
                    //Log errors or do anything you think it's needed
                    Console.WriteLine(ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            return webApp;
        }
    }
}
