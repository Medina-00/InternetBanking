

using InternetBanking.Core.Application.Dtos.Account.Request;
using InternetBanking.Core.Application.Dtos.Account.Response;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.User;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IProducto productoRepository;
        private readonly ICuentaAhorro cuentaAhorroRepository;

        public AccountService(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager ,
            IProducto productoRepository, ICuentaAhorro cuentaAhorroRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.productoRepository = productoRepository;
            this.cuentaAhorroRepository = cuentaAhorroRepository;
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
            response.Activo = user.Activo;
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
                Activo = true

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
                    var UserCreado = await userManager.FindByNameAsync(request.UserName);

                    if (UserCreado != null)
                    {
                        Producto producto = new Producto();

                        producto.Tipo = TipoProducto.CuentaAhorro.ToString();
                        producto.UserId = UserCreado.Id;
                        

                        await productoRepository.AddAsync(producto);
                        var numeroP = await productoRepository.GetByIdAsync(producto.IDProducto);

                        CuentaAhorro cuentaAhorro = new CuentaAhorro();

                        cuentaAhorro.UserId = UserCreado.Id;
                        cuentaAhorro.EsPrincipal = true;
                        cuentaAhorro.Saldo = (decimal)request.MontoInicial!;
                        cuentaAhorro.NumeroProducto = numeroP.Numero9Digitos.ToString();
                        await cuentaAhorroRepository.AddAsync(cuentaAhorro);
                    }
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

        public async Task<UpdateUserViewModel> GetByUserName(string UserId)
        {
            UpdateUserViewModel update = new();
            var result = await userManager.FindByIdAsync(UserId);

            if (result == null)
            {
                update.HasError = true;
                update.Error = $"No se Encontro el usuario Con El Siguiente Id - {UserId}";
                return update;
            }

            update.UserId = result.Id;
            update.Nombre = result.Nombre!;
            update.Apellido = result.Apellido!;
            update.UserName = result.UserName!;
            update.Email = result.Email!;

            var roles = await userManager.GetRolesAsync(result);
            update.Rol = string.Join(",", roles);


            return update;
        }

        public async Task<UpdateResponse> EditUserAsync(string UserId, UpdateRequest request)
        {
            UpdateResponse updateResponse = new();
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                updateResponse.HasError = true;
                updateResponse.Error = $"Lo Siento, No Encontre la Cuenta Con el Siguiente Id {user!.Id}";

                return updateResponse;
            }

            user.Id = request.UserId;
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

            var cuentaAhorro = await cuentaAhorroRepository.GetAllAsync();

            var cuentaAhorroUser = cuentaAhorro.Where(c => c.UserId == user.Id).FirstOrDefault();
            
            if (cuentaAhorroUser == null)
            {
                updateResponse.HasError = true;
                updateResponse.Error = $"LO SIENTO,NO SE ENCONTRO LA CUENTA DE AHORRO DEL ESTE USER!! ";
            }

            cuentaAhorroUser!.Saldo += (decimal)request.MontoInicial!;

            await cuentaAhorroRepository.UpdateAsync(cuentaAhorroUser , cuentaAhorroUser.IdCuentaAhorro);

            var resultUpdate = await userManager.UpdateAsync(user);
            if (!resultUpdate.Succeeded)
            {
                updateResponse.HasError = true;
                updateResponse.Error = $"LO SIENTO,HA OCURRIDO UN ERROR MIENTRA SE ACTUALIZABA EL USER!! ";

                return updateResponse;
            }

            return updateResponse;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUser()
        {
            var result = await userManager.Users.ToListAsync();

            var Users = new List<UserViewModel>();

            foreach (var user in result)
            {
                var roles = await userManager.GetRolesAsync(user);
                var userViewModel = new UserViewModel
                {
                    UserId = user.Id,
                    Nombre = user.Nombre!,
                    Apellido = user.Apellido!,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    Rol = string.Join(",", roles), // Concatenar los roles en una cadena separada por comas
                    Activo = user.Activo!
                };

                Users.Add(userViewModel);
            }

            return Users;
        }

        public async Task Activar(string UserId, ActivarUser activarUser)
        {

            var user = await userManager.FindByIdAsync(UserId);

            if(activarUser.Activo == "Activar")
            {
                user!.Activo = true;
            }
            else
            {
                user!.Activo = false;
            }

            
           

            await userManager.UpdateAsync(user);
        }

        
    }
}
