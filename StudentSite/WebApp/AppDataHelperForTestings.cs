using System.Security.Claims;
using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp;

public class AppDataHelperForTesting
{
    public static void SetupAppDataTesting(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
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

        
        if (configuration.GetValue<bool>("DataInitialization:SeedDataTests"))
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
                "user"
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
                ("admin@itcollege.ee","Admin", "College", "123456", "user, admin"),
                ("user@itcollege.ee","Ahmed", "Abdullajev", "123456", "user"),
                ("newuser@itcollege.ee", "User", "College", "123456", "")
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
            //add topic for testing
            var f = new Topic()
            {
                Id = new Guid(),
                Name =
                {
                    ["en-GB"] = "America",
                    ["et-EE"] = "Amerika",
                    ["ru-RU"] = "Америка",
                },
                Description =
                {
                    ["en-GB"] = "Living in America disscusion",
                    ["et-EE"] = "Ameerikas elamise arutelu",
                    ["ru-RU"] = "Обсуждение жизни в Америке",
                },
            };
            context.Topics.Add(f);
            context.SaveChanges();
            
            // ADD quiz stuff
            var subject = new Subject()
            {
                Id = new Guid(),
                Name =
                {
                    ["en-GB"] = "Math",
                    ["et-EE"] = "Matemaatika",
                    ["ru-RU"] = "Математика",
                },
                Description =
                {
                    ["en-GB"] = "Math subject",
                    ["et-EE"] = "Matematika aine",
                    ["ru-RU"] = "Математика предмет",
                },
            };

            var quiz = new Quiz()
            {
                Name =
                {
                    ["en-GB"] = "Math Quiz",
                    ["et-EE"] = "Matemaatika Kviz",
                    ["ru-RU"] = "Математика квиз",
                },
                Description =
                {
                    ["en-GB"] = "Math basics quiz",
                    ["et-EE"] = "Matematika baas kviz",
                    ["ru-RU"] = "Математика базовый квиз",
                },
                Subject = subject,
            };

            var question1 = new Question()
            {
                QuestionText =
                {
                    ["en-GB"] = "5+5?",
                },
                Quiz = quiz,
            };

            var answerOne = new Answer()
            {
                AnswerText =
                {
                    ["en-GB"] = "10",
                },
                Question = question1,
                IsCorrect = true,
            };
            var answerTwo = new Answer()
            {
                AnswerText =
                {
                    ["en-GB"] = "15",
                },
                Question = question1,
                IsCorrect = false,
            };
            
            context.Answers.Add(answerOne);
            context.Answers.Add(answerTwo);
            context.SaveChanges();
        }
        
    }

}