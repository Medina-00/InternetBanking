using InternetBanking.Core.Application.Dtos.Account.Request;
using InternetBanking.Core.Application.Dtos.Account.Response;
using InternetBanking.Core.Application.ViewModels.User;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<UpdateResponse> EditUserAsync(string userName, UpdateRequest request);
        Task<UpdateUserViewModel> GetByUserName(string Username);
        Task LogOut();
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request);
        Task SignOutAsync();

        Task<IEnumerable<UserViewModel>> GetAllUser();

        Task Activar(string UserId, ActivarUser activarUser);
    }
}