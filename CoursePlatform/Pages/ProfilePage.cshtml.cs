using CoursePlatform.Common;
using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Enums;
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
        public int CourseInProgress { get; set; }
        public int CourseCompleted { get; set; }

        public async Task OnGet()
        {
            if(User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                CurrentUser = await _context.Set<User>().Include(u => u.Profile).FirstOrDefaultAsync(u => u.UserName == currentUser.UserName);

                var role = _userManager.GetRolesAsync(currentUser).Result.FirstOrDefault();

                Role = _roleManager.FindByNameAsync(role).Result.NormalizedName;

                CourseInProgress = _context.Set<CourseEnrollment>()
                    .Include(ce => ce.Course).ThenInclude(c => c.Lectures)
                    .Include(ce => ce.Course).ThenInclude(c => c.CourseCategories)
                    .Include(ce => ce.Course).ThenInclude(c => c.Teacher).ThenInclude(t => t.Profile)
                    .Where(ce => ce.StudentId == CurrentUser.Id && ce.Progreses.Count(p => p.CompletionStatus == Status.Success) < ce.Course.Lectures.Count)
                    .Select(ce => ce.Course)
                    .Distinct()
                    .Count();

                CourseCompleted = _context.Set<CourseEnrollment>()
                    .Include(ce => ce.Course).ThenInclude(c => c.Lectures)
                    .Include(ce => ce.Course).ThenInclude(c => c.CourseCategories)
                    .Include(ce => ce.Course).ThenInclude(c => c.Teacher).ThenInclude(t => t.Profile)
                    .Where(ce => ce.StudentId == CurrentUser.Id && ce.Progreses.Count(p => p.CompletionStatus == Status.Success) == ce.Course.Lectures.Count)
                    .Select(ce => ce.Course)
                    .Distinct()
                    .Count();
            }
            else
            {
                var param = new { FilterType = "All" };

                RedirectToPage("/Index", param);
            }   
        }
    }
}
