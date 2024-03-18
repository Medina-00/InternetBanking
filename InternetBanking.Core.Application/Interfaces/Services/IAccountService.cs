using InternetBanking.Core.Application.Dtos.Account.Request;
using InternetBanking.Core.Application.Dtos.Account.Response;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<UpdateResponse> EditUserAsync(string userName, UpdateRequest request);
        Task<UpdateResponse> GetByUserName(string Username);
        Task LogOut();
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request);
        Task SignOutAsync();
    }
}