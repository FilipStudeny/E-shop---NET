using System.ComponentModel.DataAnnotations;

namespace Eshop.Shared.Models;

public class UserPasswordChange
{
    [Required, StringLength(100, MinimumLength = 5)]
    public string Password { get; set; } = string.Empty;
    [Compare("Password", ErrorMessage = "Passwords don't match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}