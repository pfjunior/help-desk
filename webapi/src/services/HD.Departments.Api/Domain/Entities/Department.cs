namespace HD.Departments.Api.Domain.Entities;

public class Department
{
    public Guid Id { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }


    public Department(string code, string name)
    {
        Id = Guid.NewGuid();
        Code = code.ToUpper();
        Name = name;
    }


    public void UpdateCode(string code) => Code = code.ToUpper();

    public void UpdateName(string name) => Name = name;
}
