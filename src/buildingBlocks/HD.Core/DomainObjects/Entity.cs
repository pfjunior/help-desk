namespace HD.Core.DomainObjects;
public abstract class Entity : IAggregateRoot
{
    public Guid Id { get; set; }

    protected Entity() => Id = Guid.NewGuid();
}
