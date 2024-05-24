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

        public IndexModel(CoursePlatformContext context)
        {
            _context = context;
            VideoUrl = "https://www.youtube.com/embed/watch?v=dQw4w9WgXcQ";
        }

        public IList<Course> Courses { get; set; }
        public IList<User> Users { get; set; }
        public string VideoUrl { get; set; }

        public void OnGet(string FilterType)
        {
            if (string.IsNullOrEmpty(FilterType) || FilterType == "All")
            {
                Courses = _context.Set<Course>()
                    .Include(c => c.Lectures)
                    .Include(c => c.Teacher).ThenInclude(t => t.Profile)
                    .Include(c => c.CourseCategories)
                    .ToList();
            }

            else if (FilterType == "InProgress")
            {
                Courses = _context.Set<CourseEnrollment>()
                    .Where(ce => ce.Student == new UserSession(_context).GetUserSession())
                    .Select(ce => ce.Course)
                    .Distinct()
                    .ToList();

                var userSession = new UserSession(_context).GetUserSession();

                /*var CoursesInProgress = */
                    
            }
            else if (FilterType == "Complete")
            {
                
            }
        }
    }
}
