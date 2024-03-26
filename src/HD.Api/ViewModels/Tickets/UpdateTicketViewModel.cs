using HD.Domain.Tickets.Enums;
using System.ComponentModel.DataAnnotations;
using Type = HD.Domain.Tickets.Enums.Type;

namespace HD.Api.ViewModels.Tickets;

public class UpdateTicketViewModel
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "O campo {0} precisa ser preenchido")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} de caracteres", MinimumLength = 2)]
    public string Title { get; set; }

    [Required(ErrorMessage = "O campo {0} precisa ser preenchido")]
    [StringLength(500, ErrorMessage = "O campo {0} deve ter entre {2} e {1} de caracteres", MinimumLength = 2)]
    public string Description { get; set; }

    public string Solution { get; set; }

    public Type Type { get; set; }

    [Required(ErrorMessage = "O campo {0} precisa ser preenchido")]
    public Status Status { get; set; }

    [Required(ErrorMessage = "O campo {0} precisa ser preenchido")]
    public Priority Priority { get; set; }

    public Guid EmployeeId { get; set; }
}
