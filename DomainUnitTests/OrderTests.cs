using Domain;

namespace DomainUnitTests;

public class OrderTests
{
    private readonly OrderDomain _orderToTest;
    public OrderTests()
    {
        _orderToTest = new OrderDomain(Guid.NewGuid(), 
            new CustomerDomain(Guid.NewGuid(), "Vitaliy Minaev"), 
            new ProductDomain(Guid.NewGuid(), "Mouse Logitech G305", 78.4m, DateTime.Now.AddYears(-1)), 
            DateTime.Now.AddDays(5), 
            false);
    }

    [Fact]
    public void CreateOrder_NullCustomerAndProduct_ThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var order = new OrderDomain(Guid.Empty, null, null, DateTime.Now, false);
        });
    }
    [Fact]
    public void CreateOrder_InvalidDeliveryDate_ThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var order = new OrderDomain(Guid.Empty, 
                new CustomerDomain(Guid.NewGuid(), "Vitaliy Minaev"), 
                new ProductDomain(Guid.NewGuid(), "Mouse Logitech G305", 78.4m, DateTime.Now.AddYears(-1)), 
                DateTime.Now.AddDays(-1), 
                false);
        });
    }

    [Fact]
    public void UpdateOrder_MarkAsDelivered_DeliveredFieldTrue()
    {
        _orderToTest.ChangeDeliveryStatus();
        bool expected = true;
        
        Assert.Equal(expected, _orderToTest.Delivered);
    }
    [Fact]
    public void UpdateOrder_ChangeDeliveryDateByAdding2Days_DeliveryDateChanged()
    {
        var expected = _orderToTest.DeliveryDate.AddDays(2);
        _orderToTest.ChangeDeliveryDate(_orderToTest.DeliveryDate.AddDays(2));
        
        Assert.Equal(expected, _orderToTest.DeliveryDate);
    }
    [Fact]
    public void UpdateOrder_ChangeDeliveryDateToInvalid_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            _orderToTest.ChangeDeliveryDate(_orderToTest.DeliveryDate.AddDays(-15));
        });
    }
}