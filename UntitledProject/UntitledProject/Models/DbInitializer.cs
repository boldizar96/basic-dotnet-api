using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UntitledProject.Models
{
    public class DbInitializer
    {
        public static void Initialize(UntitledProjectContext context, IServiceProvider serviceProvider)
        {
            if (!(context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                context.Database.Migrate();
                if (!context.Roles.Any())
                {
                    string[] roleNames = { "Admin", "User" };
                    IdentityResult roleResult;

                    foreach (var roleName in roleNames)
                    {
                        var roleExist = RoleManager.RoleExistsAsync(roleName);

                        if (!roleExist.Result)
                        {

                            roleResult = RoleManager.CreateAsync(new IdentityRole(roleName)).Result;
                        }
                    }
                }

                if (context.AppUser.Any())
                {
                    return;   // DB már fel van töltve
                }
                else
                {

                    var poweruser = new AppUser
                    {
                        UserName = "admin@admin.hu",
                        Email = "admin@admin.hu",
                        EmailConfirmed = true,
                        PhoneNumber = "01234567890",
                        PhoneNumberConfirmed = true,
                        CreatedDate = DateTime.Now,

                    };
                    string adminPassword = "Qwe_123";

                    var createPowerUser = UserManager.CreateAsync(poweruser, adminPassword);
                    if (createPowerUser.Result.Succeeded)
                    {

                        IdentityResult result = UserManager.AddToRoleAsync(poweruser, "Admin").Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception("Sikertelen db inicializálás !");
                        }

                    }
                }

            }


        }
    }
}
