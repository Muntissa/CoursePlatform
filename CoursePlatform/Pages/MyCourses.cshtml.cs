using CoursePlatform.Common;
using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoursePlatform.Common.Additional;

namespace CoursePlatform.Pages
{
    public class MyCourses : PageModel
    {
        private readonly CoursePlatformContext _context;
        private readonly UserManager<User> _userManager;

        public MyCourses(CoursePlatformContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<Course> Courses { get; set; }
        public User CurrentUser { get; set; }

        public async Task OnGet(string FilterType)
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            Courses = await _context.Set<Course>()
                .Include(c => c.Lectures)
                .Include(c => c.Teacher).ThenInclude(t => t.Profile)
                .Include(c => c.CourseCategories)
                .Where(c => c.Teacher == CurrentUser)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int courseid)
        {
            var user = await _userManager.GetUserAsync(User);

            var course = _context.Set<Course>()
                .FirstOrDefault(c => c.Id == courseid);

            // Перенаправление на ту же страницу с параметром courseid
            return RedirectToPage("/MyCourseDetail", new { courseid });
        }

    }
}
