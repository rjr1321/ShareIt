using Microsoft.AspNetCore.Identity;
using ShareIt.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Infrastructure.Identity.Seeds
{
    public class DefaultUser
    {


        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            User defaultUser = new();
            defaultUser.UserName = "basicuser";
            defaultUser.Email = "basicuser@email.com";
            defaultUser.Name = "John";
   
            defaultUser.LastName = "Doe";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;



            User defaultUser2 = new();
            defaultUser2.UserName = "basicuser2";
            defaultUser2.Email = "basicuser2@email.com";
            defaultUser2.Name = "Jane";
       
            defaultUser2.LastName = "Doe";
            defaultUser2.EmailConfirmed = true;
            defaultUser2.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultUser.Id && u.Id != defaultUser2.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                var user2 = await userManager.FindByEmailAsync(defaultUser2.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");

                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
                if (user2 == null)
                {
                    await userManager.CreateAsync(defaultUser2, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser2, Roles.Basic.ToString());
                }

            }

        }
    }
}
