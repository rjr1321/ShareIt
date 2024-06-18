using Microsoft.AspNetCore.Identity;
using ShareIt.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Infrastructure.Identity.Seeds
{
    public class DefaultAdmin
    {
        public static async Task<User> SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            User defaultUser = new();
            defaultUser.UserName = "Just4HourSleep";
            defaultUser.Email = "rjr1321@gmail.com";
            defaultUser.Name = "Ronald";
            defaultUser.LastName = "Ramirez";
     
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());

                    return user = await userManager.FindByEmailAsync(defaultUser.Email);

                }
            }

            return null;

        }
    }
}
