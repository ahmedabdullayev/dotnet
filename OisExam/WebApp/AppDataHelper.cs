using System.Security.Claims;
using App.DAL.EF;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp;

public class AppDataHelper
{
    public static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        using var serviceScope = app.
            ApplicationServices.
            GetRequiredService<IServiceScopeFactory>().
            CreateScope();

        using var context = serviceScope
            .ServiceProvider.GetService<AppDbContext>();

        if (context == null)
        {
            throw new ApplicationException("Problem in services. No db context.");
        }
        
        // TODO - Check database state
        // can't connect - wrong address
        // can't connect - wrong user/pass
        // can connect - but no database
        // can connect - there is database

        if (configuration.GetValue<bool>("DataInitialization:DropDatabase"))
        {
            context.Database.EnsureDeleted();
        }
        if (configuration.GetValue<bool>("DataInitialization:MigrateDatabase"))
        {
            context.Database.Migrate();
        }
        if (configuration.GetValue<bool>("DataInitialization:SeedIdentity"))
        {
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

            if (userManager == null || roleManager == null)
            {
                throw new NullReferenceException("userManager or roleManager cannot be null");
            }

       
            var roles = new string[]
            {
                "admin",
                "teacher",
                "student"
            };
            
            foreach (var roleInfo in roles)
            {
                var role = roleManager.FindByNameAsync(roleInfo).Result;
                if (role == null)
                {
                    var identityResult = roleManager.CreateAsync(new AppRole()
                    {
                        Name = roleInfo
                    }).Result;
                    if (!identityResult.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed");
                    }
                }
            }

            var users = new (string username,string firstName,string lastName, string password, string roles)[]
            {
                ("admin@itcollege.ee","Admin", "College", "123456", "admin"),
                ("teacher@itcollege.ee","Ahmed", "Abdullajev", "123456", "teacher"),
                ("student@itcollege.ee", "User", "College", "123456", "student")
            };


            foreach (var userInfo in users)
            {
                var user = userManager.FindByEmailAsync(userInfo.username).Result;
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Email = userInfo.username,
                        UserName = userInfo.username,
                        Firstname = userInfo.firstName,
                        Lastname = userInfo.lastName,
                        EmailConfirmed = true,
                    };
                    var identityResult = userManager.CreateAsync(user, userInfo.password).Result;
                    identityResult = userManager.AddClaimAsync(user, new Claim("aspnet.firstname", user.Firstname)).Result;
                    identityResult = userManager.AddClaimAsync(user, new Claim("aspnet.lastname", user.Lastname)).Result;
                    if (!identityResult.Succeeded)
                    {
                        throw new ApplicationException("Cannot create user!");
                    }
                }

                if (!string.IsNullOrWhiteSpace(userInfo.roles))
                {
                    var identityResultRole = userManager.AddToRolesAsync(user,
                        userInfo.roles.Split(",").Select(r => r.Trim())).Result;

                }
            }
        }

        if (configuration.GetValue<bool>("DataInitialization:SeedData"))
        {
            // TODO
            // var f = new FooBar
            // {
            //     Name =
            //     {
            //         ["en"] = "English",
            //         ["et"] = "Eesti",
            //         ["ru"] = "Русский",
            //     }
            // };
            // context.FooBars.Add(f);
            // context.SaveChanges();
        }
    }

}