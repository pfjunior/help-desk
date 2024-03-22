using System.ComponentModel.DataAnnotations;

namespace HD.Api.ViewModels.Departments;

public class CreateDepartmentViewModel
{
    [Required(ErrorMessage = "O campo {ProperpetyName} precisa ser preenchido")]
    [Range(2, 6, ErrorMessage = "O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} de caracteres")]
    public string Code { get; set; }

    [Required(ErrorMessage = "O campo {ProperpetyName} precisa ser preenchido")]
    [Range(2, 6, ErrorMessage = "O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} de caracteres")]
    public string Name { get; set; }
}
