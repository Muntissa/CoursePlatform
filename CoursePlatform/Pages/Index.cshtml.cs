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
    public class IndexModel : PageModel
    {
        private readonly CoursePlatformContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(CoursePlatformContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<Course> Courses { get; set; }
        public User CurrentUser { get; set; }

        public async Task OnGet(string FilterType)
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            if (string.IsNullOrEmpty(FilterType) || FilterType == "All")
            {
                Courses = await _context.Set<Course>()
                    .Include(c => c.Lectures)
                    .Include(c => c.Teacher).ThenInclude(t => t.Profile)
                    .Include(c => c.CourseCategories)
                    .ToListAsync();
            }

            else if (FilterType == "InProgress")
            {
                Courses = await _context.Set<CourseEnrollment>()
                    .Where(ce => ce.Student == CurrentUser)
                    .Select(ce => ce.Course)
                    .Distinct()
                    .ToListAsync();
            }
            else if (FilterType == "Complete")
            {
                
            }
        }
    }
}
