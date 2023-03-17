using OrdersApi.Messaging.Abstract;

namespace OrdersApi.Commands.DeleteCustomerCommand;

public class DeleteCustomerCommand : ICommand
{
    public Guid CustomerId { get; }
    public DeleteCustomerCommand(Guid customerId)
    {
        CustomerId = customerId;
    }
}