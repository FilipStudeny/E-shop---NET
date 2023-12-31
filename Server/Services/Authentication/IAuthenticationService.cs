﻿using Eshop.Shared.Models;
using Eshop.Shared.Models.UserModels;

namespace Eshop.Server.Services.Authentication;

public interface IAuthenticationService
{
    Task<ServiceResponse<int>> Register(User user, string password);
    Task<bool> UserExists(string email);
    Task<ServiceResponse<string>> Login(string email, string password);

    Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);

    int GetUserId();
    string GetUserEmail();
    Task<User> GetUserByEmail(string email);
    Task<Address> GetShippingAddress();
}