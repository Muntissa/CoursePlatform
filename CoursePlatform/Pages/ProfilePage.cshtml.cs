using CoursePlatform.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CoursePlatform.Pages
{
    public class ProfilePageModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public ProfilePageModel(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public User CurrentUser { get; set; }
        public string Role { get; set; }

        public async Task OnGet()
        {
            if(User.Identity.IsAuthenticated)
            {
                CurrentUser = await _userManager.GetUserAsync(User);
                var role = _userManager.GetRolesAsync(CurrentUser).Result.FirstOrDefault();

                Role = _roleManager.FindByNameAsync(role).Result.NormalizedName;
            }
                
        }
    }
}
