using Ecommerce.Server.Database;
using Ecommerce.Shared;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Ecommerce.Server.Services.UserService
{
	public class UserService : IUserService
	{
		private readonly DataContext dataContext;
		private readonly IConfiguration configuration;
		private readonly IHttpContextAccessor httpContextAccessor;

		public UserService(DataContext dataContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
			this.dataContext = dataContext;
			this.configuration = configuration;
			this.httpContextAccessor = httpContextAccessor;
		}


		public Task<ServiceResponse<bool>> ChangePassword(string Email, string newPassword)
		{
			throw new NotImplementedException();
		}

		

		public Task<User> GetUserByEmail(string Email)
		{
			throw new NotImplementedException();
		}

		public string GetUserEmail()
		{
			var userEmailString = httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
			return userEmailString!;
		}

		public int GetUserId()
		{
			var userId = httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
			return int.Parse(userId!);
		}

		public async Task<ServiceResponse<string>> Login(LoginDTO loginDTO)
		{
			var user = await dataContext.Users
				.Include(u => u.Role)
				.FirstOrDefaultAsync(user => user.Email.ToLower().Equals(loginDTO.Email.ToLower()));

			if(user == null)
			{
				return new ServiceResponse<string>
				{
					Success = false,
					Message = "User not found"
				};
			}

			if(!VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
			{
				return new ServiceResponse<string>
				{
					Success = false,
					Message = "Wrong password or email address"
				};
			}

			return new ServiceResponse<string> { Data = CreateToken(user) };

		}

		public async Task<ServiceResponse<bool>> RegisterUser(RegisterDTO registerDTO)
		{
			if(await UserExists(registerDTO.Email))
			{
				return new ServiceResponse<bool>
				{
					Data = false,
					Success = false,
					Message = "Email address already in use."
				};
			}

			if (!registerDTO.Password.Equals(registerDTO.ConfirmPassword))
			{
				return new ServiceResponse<bool>
				{
					Data = false,
					Success = false,
					Message = "Passwords do not match"
				};
			}

			CreatePasswordHash(registerDTO.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
			var customerRole = await dataContext.Roles.FirstOrDefaultAsync(role => role.Id == 3);
			if (customerRole == null)
			{
				// Handle the case where the "Customer" role is not found
				return new ServiceResponse<bool>
				{
					Data = false,
					Success = false,
					Message = "Role not found. Please check role configuration."
				};
			}

			var user = new User
			{
				Email = registerDTO.Email,
				PasswordHash = PasswordHash,
				PasswordSalt = PasswordSalt,
				Role = customerRole,
				RoleId = customerRole.Id
			};

			dataContext.Users.Add(user);
			await dataContext.SaveChangesAsync();

			return new ServiceResponse<bool> { Data = true, Message = "Registration succesfull" };
		}

		public async Task<bool> UserExists(string Email)
		{
			var user = await dataContext.Users.FirstOrDefaultAsync(user => user.Email.ToLower().Equals(Email.ToLower()));
			return user == null ? false : true;
		
		}

		public bool VerifyPasswordHash(string Password, IEnumerable<byte> PasswordHash, byte[] PasswordSalt)
		{
			using var hmac = new HMACSHA512(PasswordSalt);
			var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
			return hash.SequenceEqual(PasswordHash);
		}

		public void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
		{
			using var hmac = new HMACSHA512();
			PasswordSalt = hmac.Key;
			PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
		}

		public string CreateToken(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Role, user.Role.Name),
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value!));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: credentials
			);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);
			return jwt;
		}
	}
}
