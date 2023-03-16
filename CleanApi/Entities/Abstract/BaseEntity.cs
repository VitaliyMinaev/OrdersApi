namespace CleanApi.Entities.Abstract;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;
}