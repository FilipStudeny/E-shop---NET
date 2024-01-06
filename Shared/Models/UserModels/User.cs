using Eshop.Shared.Models.UserModels;

namespace Eshop.Shared.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public Address Address { get; set; }
    public string Role { get; set; } = "Customer";
}