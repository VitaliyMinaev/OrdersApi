namespace Domain.Abstract;

public abstract class BaseDomain
{
    public Guid Id { get; }

    public BaseDomain(Guid id)
    {
        Id = id;
    }
}