using OrdersApi.Contracts.Responses;
using OrdersApi.Messaging.Abstract;

namespace OrdersApi.Commands.UpdateCustomerCommand;

public class UpdateCustomerCommand : ICommand<CustomerResponse>
{
    public Guid CustomerId { get; }
    public string FullName { get; }
    public UpdateCustomerCommand(Guid customerId, string fullName)
    {
        FullName = fullName;
        CustomerId = customerId;
    }
}