using System.ComponentModel.DataAnnotations;

namespace OrdersApi.Entities.Abstract;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;
}