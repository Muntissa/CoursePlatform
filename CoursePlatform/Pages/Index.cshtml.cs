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
        public string CurrentFilterType { get; set; }

        public async Task OnGet(string FilterType, string searchTerm)
        {
            if (!string.IsNullOrEmpty(FilterType))
                CurrentFilterType = FilterType;

            CurrentUser = await _userManager.GetUserAsync(User);

            if(CurrentUser == null)
            {
                Courses = await _context.Set<Course>()
                    .Include(c => c.Lectures)
                    .Include(c => c.CourseCategories)
                    .Include(c => c.Teacher).ThenInclude(t => t.Profile)
                    .Include(c => c.Teacher).ToListAsync();

                return;
            }

            IQueryable<Course> query = _context.Set<Course>()
                .Include(c => c.Lectures)
                .Include(c => c.Teacher).ThenInclude(t => t.Profile)
                .Include(c => c.CourseCategories);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var normilizeST = searchTerm.ToLower();
                query = query
                    .Where(c => c.CourseTitle.ToLower().Contains(normilizeST) 
                    || c.CourseDecription.ToLower().Contains(normilizeST)
                    || c.Teacher.Profile.Surname.ToLower().Contains(normilizeST));
            }

            if (string.IsNullOrEmpty(FilterType) || FilterType == "All")
            {
                var enrolledCourseIds = await _context.Set<CourseEnrollment>()
                    .Include(ce => ce.Student)
                    .Where(ce => ce.Student.Id == CurrentUser.Id)
                    .Select(ce => ce.CourseId)
                    .Distinct()
                    .ToListAsync();

                Courses = await query
                    .Where(c => !enrolledCourseIds.Contains(c.Id))
                    .ToListAsync();
            }
            else if (FilterType == "InProgress")
            {
                var courses = await _context.Set<CourseEnrollment>()
                    .Include(ce => ce.Course).ThenInclude(c => c.Lectures)
                    .Include(ce => ce.Course).ThenInclude(c => c.CourseCategories)
                    .Include(ce => ce.Course).ThenInclude(c => c.Teacher).ThenInclude(t => t.Profile)
                    .Where(ce => ce.StudentId == CurrentUser.Id && ce.Progreses.Count(p => p.CompletionStatus == Status.Success) < ce.Course.Lectures.Count)
                    .Select(ce => ce.Course)
                    .Distinct()
                    .ToListAsync();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    courses = courses.Where(c => c.CourseTitle.Contains(searchTerm) || c.CourseDecription.Contains(searchTerm)).ToList();
                }

                Courses = courses;
            }
            else if (FilterType == "Complete")
            {
                var courses = await _context.Set<CourseEnrollment>()
                    .Include(ce => ce.Course).ThenInclude(c => c.Lectures)
                    .Include(ce => ce.Course).ThenInclude(c => c.CourseCategories)
                    .Include(ce => ce.Course).ThenInclude(c => c.Teacher).ThenInclude(t => t.Profile)
                    .Where(ce => ce.StudentId == CurrentUser.Id && ce.Progreses.Count(p => p.CompletionStatus == Status.Success) == ce.Course.Lectures.Count)
                    .Select(ce => ce.Course)
                    .Distinct()
                    .ToListAsync();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    courses = courses.Where(c => c.CourseTitle.Contains(searchTerm) || c.CourseDecription.Contains(searchTerm)).ToList();
                }

                Courses = courses;
            }
            else
            {
                var enrolledCourseIds = await _context.Set<CourseEnrollment>()
                    .Include(ce => ce.Student)
                    .Where(ce => ce.Student.Id == CurrentUser.Id)
                    .Select(ce => ce.CourseId)
                    .Distinct()
                    .ToListAsync();

                Courses = await query
                    .Where(c => !enrolledCourseIds.Contains(c.Id))
                    .ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostUnsubcribeAsync(int courseid)
        {
            var user = await _userManager.GetUserAsync(User);

            var course = _context.Set<Course>()
                .Include(c => c.CourseEnrollments).ThenInclude(ce => ce.Progreses)
                .Include(c => c.CourseEnrollments).ThenInclude(ce => ce.Certificate)
                .FirstOrDefault(c => c.Id == courseid);

            var ceToDelete = course.CourseEnrollments.FirstOrDefault(ce => ce.Student.Id == user.Id);

            _context.Remove(ceToDelete.Certificate);
            _context.RemoveRange(ceToDelete.Progreses);
            _context.Remove(ceToDelete);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Index", new { FilterType = "All" });
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
