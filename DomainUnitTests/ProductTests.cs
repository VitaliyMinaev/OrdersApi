using Domain;

namespace DomainUnitTests;

public class ProductTests
{
    [Fact]
    public void ValidateProduct_NegativePrice_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var product = new ProductDomain(Guid.Empty, "LED Flashlight AIO5RH", -90, DateTime.Now.AddDays(-1));
        });
    }
    [Fact]
    public void ValidateProduct_ReleaseDateEqualToTomorrow_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var product = new ProductDomain(Guid.Empty, "LED Flashlight AIO5RH", 90, DateTime.Now.AddDays(1));
        });
    }
}