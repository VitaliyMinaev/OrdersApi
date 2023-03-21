namespace OrdersApi.Contracts.Responses.Customer;

public class GetCustomerResponse 
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public List<Guid>? OrdersId { get; set; }
}