using CleanApi.Entities.Abstract;

namespace CleanApi.Entities;

public class ProductEntity : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
}