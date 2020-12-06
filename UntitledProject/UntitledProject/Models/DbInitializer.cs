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
          
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            context.Database.EnsureCreated();
            if (!context.UserRoles.Any())
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

            if (!context.AppUser.Any())
            {
                  // DB már fel van töltv

                var poweruser = new AppUser
                {
                    FirstName = "Admin",
                    LastName = "Admin",
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

            if (!context.Category.Any())
            {
                string[] categories = { "PC", "Apple", "IoT", "Gaming", "Notebook", "Android", "Industry", "Accessories", "Others" };

                foreach (var item in categories)
                {
                    Category temp = new Category { Name = item };

                    context.Category.Add(temp);
                }

                context.SaveChanges();
            }

            if(!context.Product.Any())
            {

                ICollection<Category> cats = context.Category.Where(c => c.Name == "Pc").ToList();
                Product temp1 = new Product { Categories = cats, Condition = Condition.New, Description = "asd", ProductName = "FirstTest" };
                Product temp2 = new Product { Categories = cats, Condition = Condition.New, Description = "asd", ProductName = "SecondTest" };
                Product temp3 = new Product { Categories = cats, Condition = Condition.New, Description = "asd", ProductName = "ThirdTest"};
                Product temp4 = new Product { Categories = cats, Condition = Condition.New, Description = "asd", ProductName = "FourthTest" };
                Product temp5 = new Product { Categories = cats, Condition = Condition.New, Description = "asd", ProductName = "FifthTest" };

                context.Add(temp1);
                context.Add(temp2);
                context.Add(temp3);
                context.Add(temp4);
                context.Add(temp5);
                context.SaveChanges();
            }


        }
    }
}
