using System.ComponentModel.DataAnnotations;
using Type = HD.Domain.Tickets.Enums.Type;

namespace HD.Api.ViewModels.Tickets;

public class CreateTicketViewModel
{
    [Required(ErrorMessage = "O campo {ProperpetyName} precisa ser preenchido")]
    [Range(2, 100, ErrorMessage = "O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} de caracteres")]
    public string Title { get; set; }

    [Required(ErrorMessage = "O campo {ProperpetyName} precisa ser preenchido")]
    [Range(2, 500, ErrorMessage = "O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} de caracteres")]
    public string Description { get; set; }

    [Required(ErrorMessage = "O campo {ProperpetyName} precisa ser preenchido")]
    public Type Type { get; set; }
}
