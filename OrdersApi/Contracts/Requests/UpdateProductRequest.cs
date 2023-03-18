using System.ComponentModel.DataAnnotations;

namespace OrdersApi.Contracts.Requests;

public class UpdateProductRequest
{
    [Required]
    public string Name { get; set; }
    public decimal Price { get; set; }
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
}
