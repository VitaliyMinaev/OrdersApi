namespace CleanApi.Entities.Abstract;

public abstract class EntityBase
{
    public Guid Id { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;
}