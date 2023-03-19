using Microsoft.EntityFrameworkCore;
using OrdersApi.Persistence;
using OrdersApi.Persistence.Abstract;

namespace OrdersApi.Installers;

public static class InstallerDatabaseExtension
{
    public static void InstallSqliteDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDatabaseContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("SqliteConnection"));
        });
    }

    public static void InstallMigrations(this IApplicationBuilder applicationBuilder, ILogger logger)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDatabaseContext>();

            if (context == null)
            {
                logger.LogError("Context is null. Can not apply migration");
                return;
            }

            ApplyMigration(context, logger);
        }
    }

    private static void ApplyMigration(ApplicationDatabaseContext context, ILogger logger)
    {
        var migrationInstaller = new DatabaseMigrationInstaller(context, logger);
        migrationInstaller.InstallMigrations();
    }

    public static async Task LoadDataAsync(this IApplicationBuilder applicationBuilder, ILogger logger)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var dataLoader = serviceScope.ServiceProvider.GetService<IDataLoader>();

            if (dataLoader == null)
            {
                logger.LogError($"{nameof(dataLoader)} is null. Can not load data");
                return;
            }

            await dataLoader.LoadDataAsync();
        }
    }
}