namespace OrdersApi.Entities.Abstract;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.Now;
}