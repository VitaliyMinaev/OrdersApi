namespace OrdersApi.Persistence.Abstract;

public interface IDatabaseMigrationInstaller
{
    void InstallMigrations();
}