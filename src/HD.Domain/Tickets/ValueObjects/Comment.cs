namespace HD.Domain.Tickets.ValueObjects;

public class Comment
{
    public Comment(string description, Guid employeeId)
    {
        Description = description;
        CreatedIn = DateTime.Now;
        EmployeeId = employeeId;
    }

    public string Description { get; private set; }
    public DateTime CreatedIn { get; private set; }

    public Guid EmployeeId { get; private set; }
}
