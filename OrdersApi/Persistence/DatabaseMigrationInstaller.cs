using Microsoft.EntityFrameworkCore;
using OrdersApi.Persistence.Abstract;

namespace OrdersApi.Persistence;

public class DatabaseMigrationInstaller : IDatabaseMigrationInstaller
{
    private readonly ILogger _logger;
    private readonly ApplicationDatabaseContext _dataContext;
    public DatabaseMigrationInstaller(ApplicationDatabaseContext dataContext, ILogger logger)
    {
        _dataContext = dataContext;
        _logger = logger;
    }

    public void InstallMigrations()
    {
        _logger.LogInformation("Applying migration ...");
        _dataContext.Database.Migrate();
    }
}