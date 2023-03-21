using OrdersApi.Contracts.Responses;
using OrdersApi.Messaging.Abstract;

namespace OrdersApi.Commands.CreateCustomerCommand;

public class CreateCustomerCommand : ICommand<CustomerModel>
{
    public string FullName { get; }
    public CreateCustomerCommand(string fullName)
    {
        FullName = fullName;
    }
}