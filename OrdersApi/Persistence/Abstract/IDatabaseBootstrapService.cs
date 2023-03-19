namespace OrdersApi.Persistence.Abstract;

public interface IDatabaseBootstrapService
{
    Task SetupAsync();
}