namespace HD.Core.DomainObjects;
public abstract class Entity : IAggregateRoot
{
    public Guid Id { get; set; }
    public DateTime CreatedIn { get; set; }
    public DateTime? UpdatedIn { get; set; }


    protected Entity() => Id = Guid.NewGuid();
}
