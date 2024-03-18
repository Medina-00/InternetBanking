﻿

using InternetBanking.Core.Application.Dtos.Account.Response;
using InternetBanking.Core.Application.ViewModels.User;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm);
        Task SignOutAsync();
        Task<UpdateResponse> UpdateUserAsync(string userId, UpdateUserViewModel request);
        Task<UpdateResponse> GetByUserName(string UserName);

    }
}