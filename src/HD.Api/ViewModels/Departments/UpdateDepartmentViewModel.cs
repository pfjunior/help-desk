using System.ComponentModel.DataAnnotations;

namespace HD.Api.ViewModels.Departments;

public class UpdateDepartmentViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} precisa ser preenchido")]
    [StringLength(6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} de caracteres", MinimumLength = 2)]
    public string Code { get; set; }

    [Required(ErrorMessage = "O campo {0} precisa ser preenchido")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} de caracteres", MinimumLength = 2)]
    public string Name { get; set; }
}
