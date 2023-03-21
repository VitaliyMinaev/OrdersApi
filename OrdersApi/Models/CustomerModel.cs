namespace OrdersApi.Contracts.Responses;

public class CustomerModel
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public List<Guid>? OrdersId { get; set; }
}