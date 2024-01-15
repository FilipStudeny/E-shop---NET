using System.Security.Claims;

namespace Ecommerce.Client.Security
{
    public class UserData
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(
            new Claim[]
            {
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.Hash, Password),
                new Claim(ClaimTypes.Role, Role),
            },"JWT"));

        public static UserData FromClaimsPrincipal(ClaimsPrincipal principal) => new()
        {
            Password = principal.FindFirst(ClaimTypes.Hash)?.Value ?? "",
            Email = principal.FindFirst(ClaimTypes.Email)?.Value ?? "",
            Role = principal.FindFirst(ClaimTypes.Role)?.Value ?? "Customer",
        };

    }
}
