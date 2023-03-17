using CleanApi.Contracts.Responses;
using CleanApi.Messaging.Abstract;

namespace CleanApi.Commands.CreateCustomerCommand;

public class CreateCustomerCommand : ICommand<CustomerResponse>
{
    public string FullName { get; }
    public CreateCustomerCommand(string fullName)
    {
        FullName = fullName;
    }
}