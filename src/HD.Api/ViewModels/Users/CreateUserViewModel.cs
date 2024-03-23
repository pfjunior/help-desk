using System.ComponentModel.DataAnnotations;

namespace HD.Api.ViewModels.Users;

public class CreateUserViewModel
{
    [Required(ErrorMessage = "O campo {ProperpetyName} precisa ser preenchido")]
    [Range(2, 8, ErrorMessage = "O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} de caracteres")]
    public string Registration { get; set; }

    [Required(ErrorMessage = "O campo {ProperpetyName} precisa ser preenchido")]
    [Range(2, 30, ErrorMessage = "O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} de caracteres")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "O campo {ProperpetyName} precisa ser preenchido")]
    [Range(2, 30, ErrorMessage = "O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} de caracteres")]
    public string LastName { get; set; }

    public string? Extension { get; set; }

    [Required(ErrorMessage = "O campo {ProperpetyName} precisa ser preenchido")]
    public string Department { get; set; }
}
