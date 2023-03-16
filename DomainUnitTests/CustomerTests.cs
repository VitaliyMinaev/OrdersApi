using Domain;
using Xunit.Abstractions;

namespace DomainUnitTests;

public class CustomerTests
{
    private readonly CustomerDomain _customerToTest;
    private readonly ITestOutputHelper _output;
    public CustomerTests(ITestOutputHelper output)
    {
        _output = output;
        _customerToTest = new(Guid.NewGuid(), "Vitaliy Minaev");
    }
    
    [Fact]
    public void OrderProduct_OrderNullProduct_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            _customerToTest.OrderProduct(null);
        });
    }
    [Fact]
    public void OrderProduct_OrderAppropriateProduct_AppropriateOrder()
    {
        var order = _customerToTest.OrderProduct(new ProductDomain(Guid.NewGuid(), "Lenovo L340", 
            780.4m, DateTime.Now.AddYears(-1)));
        
        Assert.NotNull(order);
        _output.WriteLine("Not null passed");
        Assert.NotEqual(Guid.Empty, order.Id);
        _output.WriteLine("Not equal to Guid.Empty passed");
    }
}