namespace HD.Api.ViewModels.Users;

public class UserViewModel
{
    public Guid Id { get; set; }
    public string Registration { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Extension { get; set; }
    public string? DepartmentName { get; set; }
}
