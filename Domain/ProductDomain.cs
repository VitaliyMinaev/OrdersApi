using Domain.Abstract;

namespace Domain;

public class ProductDomain : BaseDomain
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    
    public ProductDomain(Guid id, string name, decimal price, DateTime releaseDate) : base(id)
    {
        if (price < 0)
            throw new ArgumentException($"{nameof(Price)} can not be less than zero");
        if (releaseDate > DateTime.Now)
            throw new ArgumentException($"{nameof(ReleaseDate)} Can not be greater than current date");
        
        Name = name;
        Price = price;
        ReleaseDate = releaseDate;
    }
}