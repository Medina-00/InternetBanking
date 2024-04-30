using InternetBanking.Infrastructure.Identity;
using InternetBanking.Core.Application;
using InternetBanking.Infrastructure.Identity.Entities;
using InternetBanking.Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using InternetBanking.Infrastructure.Persistence;
internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddSession();
        builder.Services.AddIdentityInfrastructure(builder.Configuration);
        builder.Services.AddApplicationLayer(builder.Configuration);
        builder.Services.AddPersistenceInfrastructure(builder.Configuration);


        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await DefaultRoles.SeedAsync(userManager, roleManager);
                await DefaultSuperAdminUser.SeedAsync(userManager, roleManager);
                await DefaultClienteUser.SeedAsync(userManager, roleManager);
                await DefaultAdminUser.SeedAsync(userManager, roleManager);

            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseSession();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        //(OJO) EN ESTE PARTE SIEMPRE HAY QUE ASEGURARSE QUE PRIMERO SE AUTHENTIQUE Y DESPUES AUTORIZE.
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=User}/{action=Login}/{id?}");

        app.Run();
    }
}