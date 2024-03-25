using System.ComponentModel.DataAnnotations;

namespace HD.Api.ViewModels.Employees;

public class UpdateEmployeeViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} precisa ser preenchido")]
    [StringLength(8, ErrorMessage = "O campo {0} deve ter entre {2} e {1} de caracteres", MinimumLength = 2)]
    public string Registration { get; set; }

    [Required(ErrorMessage = "O campo {0} precisa ser preenchido")]
    [StringLength(30, ErrorMessage = "O campo {0} deve ter entre {2} e {1} de caracteres", MinimumLength = 2)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "O campo {0} precisa ser preenchido")]
    [StringLength(30, ErrorMessage = "O campo {0} deve ter entre {2} e {1} de caracteres", MinimumLength = 2)]
    public string LastName { get; set; }

    public string? Extension { get; set; }

    [Required(ErrorMessage = "O campo {0} precisa ser preenchido")]
    public Guid DepartmentId { get; set; }
}
