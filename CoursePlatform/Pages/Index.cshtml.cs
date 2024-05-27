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
                var courses = await _context.Set<CourseEnrollment>()
    .Include(ce => ce.Course)
        .ThenInclude(c => c.Lectures)
    .Include(ce => ce.Course)
        .ThenInclude(c => c.CourseCategories)
    .Include(ce => ce.Course)
                    .ThenInclude(c => c.Teacher)
                .ThenInclude(t => t.Profile)
                .Where(ce => ce.StudentId == CurrentUser.Id &&(ce.Progreses.Any(p => p.CompletionStatus == Status.InProgress) || ce.Progreses.Count() == 0))
                .Select(ce => ce.Course)
                .Distinct()
                .ToListAsync();

                Courses = courses;
            }
            else if (FilterType == "Complete")
            {
                var courses = await _context.Set<CourseEnrollment>()
                    .Include(ce => ce.Course).ThenInclude(c => c.Lectures)
                    .Include(ce => ce.Course).ThenInclude(c => c.CourseCategories)
                    .Include(ce => ce.Course).ThenInclude(c => c.Teacher).ThenInclude(t => t.Profile)
                    .Where(ce => ce.Progreses.Any(p => p.CompletionStatus == Status.Success) || ce.Progreses.Count() == ce.Course.Lectures.Count())
                    .Select(ce => ce.Course)
                    .Distinct()
                    .ToListAsync();

                Courses = courses;
            }

        }

        public async Task<IActionResult> OnPostAsync(int courseid)
        {
            var user = await _userManager.GetUserAsync(User);

            var course = _context.Set<Course>()
                .Include(c => c.CourseEnrollments)
                .FirstOrDefault(c => c.Id == courseid);

            if (course != null)
            {
                course.CourseEnrollments.Add(new CourseEnrollment() { EnrollmentDate = DateTime.Now, Student = user });
                await _context.SaveChangesAsync();
            }

            // Перенаправление на ту же страницу с параметром courseid
            return RedirectToPage(new { courseid });
        }

    }
}
