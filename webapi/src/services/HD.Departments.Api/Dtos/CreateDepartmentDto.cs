using System.ComponentModel.DataAnnotations;

namespace HD.Departments.Api.Dtos;

public class CreateDepartmentDto
{
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(8, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 2)]
    public string Code { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(60, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 2)]
    public string Name { get; set; }
}


