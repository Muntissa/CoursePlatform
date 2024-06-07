using CoursePlatform.Common;
using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.WebApi
{
    public static class Busket
    {
        public static async Task FillDataBase(UserManager<User> _userManager, RoleManager<Role> _roleManager)
        {
            var adminOne = await _userManager.FindByNameAsync("FirstAdmin");
            if (adminOne == null)
            {
                adminOne = new User { UserName = "FirstAdmin" };
                var resultOne = await _userManager.CreateAsync(adminOne, "FirstAdmin1!");
                if (resultOne.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                        await _roleManager.CreateAsync(new Role { Name = "Admin" });

                    await _userManager.AddToRoleAsync(adminOne, "Admin");
                }
            }

            var adminTwo = await _userManager.FindByNameAsync("SecondAdmin");
            if (adminTwo == null)
            {
                adminTwo = new User { UserName = "SecondAdmin" };
                var resultTwo = await _userManager.CreateAsync(adminTwo, "SecondAdmin1!");
                if (resultTwo.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                        await _roleManager.CreateAsync(new Role { Name = "Admin" });

                    await _userManager.AddToRoleAsync(adminTwo, "Admin");
                }
            }
        }
    }
}
