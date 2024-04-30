
using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account.Request;
using InternetBanking.Core.Application.Dtos.Account.Response;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.User;

namespace InternetBanking.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IAccountService accountService;

        public UserService(IMapper mapper , IAccountService accountService)
        {
            this.mapper = mapper;
            this.accountService = accountService;
        }

        public async Task Activar(string UserId, ActivarUser activarUser)
        {
            await accountService.Activar(UserId, activarUser);  
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUser()
        {
            return await accountService.GetAllUser();
        }

        public async Task<UpdateUserViewModel> GetByUserId(string UserName)
        {
            return await accountService.GetByUserName(UserName);
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest request = mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse response = await accountService.AuthenticateAsync(request);
            return response;
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm)
        {
            RegisterRequest request = mapper.Map<RegisterRequest>(vm);

            return await accountService.RegisterUserAsync(request);
        }

        public async Task SignOutAsync()
        {
            await accountService.LogOut();
        }

        public async Task<UpdateResponse> UpdateUserAsync(string userId, UpdateUserViewModel updateUser)
        {
            UpdateRequest request = mapper.Map<UpdateRequest>(updateUser);

            return await accountService.EditUserAsync(userId, request);
        }

    }
}
