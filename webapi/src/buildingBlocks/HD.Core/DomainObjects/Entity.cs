namespace HD.Core.DomainObjects;
public abstract class Entity : IAggregateRoot
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }


    protected Entity() => Id = Guid.NewGuid();
}
