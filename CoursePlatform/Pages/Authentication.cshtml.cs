using CoursePlatform.Common;
using CoursePlatform.Common.Additional;
using CoursePlatform.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.WebApi.Pages
{
    public class AuthenticationModel : PageModel
    {   
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly CoursePlatformContext _context;

        public AuthenticationModel(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, CoursePlatformContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async void OnGet()
        {
        }
        
        public async Task<IActionResult> OnPostRegistrationAsync(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = username };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("Student"))
                    {
                        await _roleManager.CreateAsync(new Role { Name = "Student" });
                    }

                    await _userManager.AddToRoleAsync(user, "Student");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToPage("/ProfilePage");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            var param = new { FilterType = "All" };

            return RedirectToPage("/Index", param);
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await _signInManager.SignOutAsync();

            var param = new { FilterType = "All" };

            return RedirectToPage("/Index", param);
        }
        
        public async Task<IActionResult> OnPostLoginAsync(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);

                if (result.IsLockedOut)
                    ModelState.AddModelError(string.Empty, "User account locked out.");
            }

            var param = new { FilterType = "All" };

            return RedirectToPage("/Index", param);
        }

        public async Task<IActionResult> OnPostEditAccountAsync(string currentusername, string newusername, string newpassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(currentusername);
                if (user != null)
                {
                    if(!String.IsNullOrEmpty(newusername))
                        user.UserName = newusername;

                    var result = await _userManager.UpdateAsync(user);
                    
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(newpassword))
                        {
                            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                            var passwordChangeResult = await _userManager.ResetPasswordAsync(user, token, newpassword);
                            
                            if (!passwordChangeResult.Succeeded)
                            {
                                foreach (var error in passwordChangeResult.Errors)
                                    ModelState.AddModelError(string.Empty, error.Description);
                                
                                return RedirectToPage("/ProfilePage");
                            }
                        }

                        return RedirectToPage("/ProfilePage");
                    }
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                else
                    ModelState.AddModelError(string.Empty, "User not found.");
            }

            var param = new { FilterType = "All" };

            return RedirectToPage("/Index", param);
        }

        public async Task<IActionResult> OnPostEditProfileAsync(string currentusername, string surname, string name, string lastname)
        {
            var user = await _context.Set<User>().Include(u => u.Profile).FirstOrDefaultAsync(u => u.UserName == currentusername);

            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(surname) && !String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(lastname))
                {
                    var profile = _context.Set<Profile>().FirstOrDefault(p => p.User == user);

                    user.Profile.Surname = surname.RemoveSpecAndNum();
                    user.Profile.Name = name.RemoveSpecAndNum();
                    user.Profile.LastName = lastname.RemoveSpecAndNum();

                    _context.Attach(profile).State = EntityState.Modified;
                    _context.Attach(profile).State = EntityState.Modified;

                    await _context.SaveChangesAsync();

                    var result = await _userManager.UpdateAsync(user);

                    var userToTest = _context.Set<User>().Where(u => u.UserName == currentusername).Include(u => u.Profile).FirstOrDefault();
                }
            }

            return RedirectToPage("/ProfilePage");
        }

    }

}
