namespace OrdersApi.Persistence.Abstract;

public interface IDataLoader
{
    Task LoadDataAsync();
}