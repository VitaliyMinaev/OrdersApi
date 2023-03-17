using OrdersApi.Entities.Abstract;

namespace OrdersApi.Entities;

public class CustomerEntity : BaseEntity
{
    public string FullName { get; set; }
}