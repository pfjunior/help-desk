using System.ComponentModel.DataAnnotations;

namespace HD.Identity.Api.Models.ViewModels;

public class UserRegisterViewModel
{
    [Required(ErrorMessage = "{0} is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid {0} address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(42, ErrorMessage = "The {0} must have between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public string ConfirmPassword { get; set; }
}