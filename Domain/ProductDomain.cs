using Domain.Abstract;

namespace Domain;

public class ProductDomain : BaseDomain
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    
    public ProductDomain(Guid id, string name, decimal price, DateTime releaseDate) : base(id)
    {
        Name = name;
        Price = price;
        ReleaseDate = releaseDate;
    }
}