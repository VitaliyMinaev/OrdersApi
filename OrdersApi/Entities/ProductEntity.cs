using System.ComponentModel.DataAnnotations;
using OrdersApi.Entities.Abstract;

namespace OrdersApi.Entities;

public class ProductEntity : BaseEntity
{
    [Required]
    public string Name { get; set; }
    public decimal Price { get; set; }
    [Required]
    public DateTime ReleaseDate { get; set; }
}