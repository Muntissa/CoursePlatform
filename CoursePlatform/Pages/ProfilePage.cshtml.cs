using CoursePlatform.Common;
using CoursePlatform.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CoursePlatform.Pages
{
    public class ProfilePageModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly CoursePlatformContext _context;

        public ProfilePageModel(UserManager<User> userManager, RoleManager<Role> roleManager, CoursePlatformContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public User CurrentUser { get; set; }
        public string Role { get; set; }

        public async Task OnGet()
        {
            if(User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                CurrentUser = await _context.Set<User>().Include(u => u.Profile).FirstOrDefaultAsync(u => u.UserName == currentUser.UserName);

                var role = _userManager.GetRolesAsync(currentUser).Result.FirstOrDefault();

                Role = _roleManager.FindByNameAsync(role).Result.NormalizedName;
            }
            else
            {
                var param = new { FilterType = "All" };

                RedirectToPage("/Index", param);
            }   
        }
    }
}
