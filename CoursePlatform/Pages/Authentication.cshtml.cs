using CoursePlatform.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoursePlatform.WebApi.Pages
{
    public class AuthenticationModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;

        public AuthenticationModel(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public void OnGet()
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
                if (result.Succeeded)
                {
                    return RedirectToPage("/ProfilePage");
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "User account locked out.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            var param = new { FilterType = "All" };

            return RedirectToPage("/Index", param);
        }

        public async Task<IActionResult> OnPostEditAccountAsync(string currentUsername, string newUsername, string newPassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(currentUsername);
                if (user != null)
                {
                    user.UserName = newUsername;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(newPassword))
                        {
                            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                            var passwordChangeResult = await _userManager.ResetPasswordAsync(user, token, newPassword);
                            if (!passwordChangeResult.Succeeded)
                            {
                                foreach (var error in passwordChangeResult.Errors)
                                {
                                    ModelState.AddModelError(string.Empty, error.Description);
                                }
                                return RedirectToPage("/ProfilePage");
                            }
                        }

                        return RedirectToPage("/ProfilePage");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            var param = new { FilterType = "All" };

            return RedirectToPage("/Index", param);
        }
    }

}
