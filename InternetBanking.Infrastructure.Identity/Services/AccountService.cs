

using InternetBanking.Core.Application.Dtos.Account.Request;
using InternetBanking.Core.Application.Dtos.Account.Response;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace InternetBanking.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Existe un Cuenta registrada con el siguiente Email - {request.Email}";
                return response;
            }

            var result = await signInManager.PasswordSignInAsync(user.UserName!, request.Password, true, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Las Credenciales Son Incorrectas, Vuelva a Intentar.";
                return response;
            }


            response.Id = user.Id;
            response.Email = user.Email!;
            response.UserName = user.UserName!;

            var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var UserNameExistente = await userManager.FindByNameAsync(request.UserName);
            if (UserNameExistente != null)
            {
                response.HasError = true;
                response.Error = $"Ya esta en uso este Username, Ingrese uno Diferente.";
                return response;
            }

            var EmailExistente = await userManager.FindByEmailAsync(request.Email);
            if (EmailExistente != null)
            {
                response.HasError = true;
                response.Error = $"Ya esta en uso este Email, Ingrese uno Diferente.";
                return response;
            }

            var user = new ApplicationUser
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,
                UserName = request.UserName,
                EmailConfirmed = true,

            };

            var result = await userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                if (request.Rol == "Administrador")
                {
                    await userManager.AddToRoleAsync(user, Roles.Administrador.ToString());

                }
                else if (request.Rol == "Cliente")
                {
                    await userManager.AddToRoleAsync(user, Roles.Cliente.ToString());
                }

            }
            else
            {
                response.HasError = true;
                response.Error = $"Ocurrio Un Error Mientras Se Creaba El Usuario.";
                return response;
            }

            return response;
        }
        public async Task LogOut()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<UpdateResponse> GetByUserName(string Username)
        {
            UpdateResponse response = new();
            var result = await userManager.FindByNameAsync(Username);

            if (result == null)
            {
                response.HasError = true;
                response.Error = $"No se Encontro el usuario Con El Siguiente UserName - {Username}";
                return response;
            }

            response.UserId = result.Id;
            response.Nombre = result.Nombre!;
            response.Apellido = result.Apellido!;
            response.UserName = result.UserName!;
            response.Email = result.Email!;

            return response;
        }

        public async Task<UpdateResponse> EditUserAsync(string userName, UpdateRequest request)
        {
            UpdateResponse updateResponse = new();
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                updateResponse.HasError = true;
                updateResponse.Error = $"Lo Siento, No Encontre la Cuenta Con el Siguiente Id {user!.Id}";

                return updateResponse;
            }

            user.Nombre = request.Nombre;
            user.Apellido = request.Apellido;
            user.UserName = request.UserName;
            user.Email = request.Email;

            var token = await userManager.GeneratePasswordResetTokenAsync(user!);

            if (request.Password == null)
            {
                var result = await userManager.ResetPasswordAsync(user, token, request.Password!);
                if (!result.Succeeded)
                {
                    updateResponse.HasError = true;
                    updateResponse.Error = $"LO SIENTO,HA OCURRIDO UN ERROR MIENTRA SE CAMBIABA LA CONTRASEÑA!! ";

                    return updateResponse;
                }
            }


            var resultUpdate = await userManager.UpdateAsync(user);
            if (!resultUpdate.Succeeded)
            {
                updateResponse.HasError = true;
                updateResponse.Error = $"LO SIENTO,HA OCURRIDO UN ERROR MIENTRA SE ACTUALIZABA EL USER!! ";

                return updateResponse;
            }

            return updateResponse;
        }




    }
}
