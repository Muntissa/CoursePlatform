using CoursePlatform.Common;
using CoursePlatform.Common.Additional;
using CoursePlatform.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Pages
{
    public class ProfilePage : PageModel
    {
        private readonly CoursePlatformContext _context;

        public ProfilePage(CoursePlatformContext context)
        {
            _context = context;
        }

        public Course course;

        public void OnGet(int? id)
        {
            if (id is null)
                return;

            course = _context.Set<Course>().Where(c => c.Id == id)
                .Include(c => c.Lectures)
                .Include(c => c.Teacher)
                .ThenInclude(t => t.Profile)
                .FirstOrDefault();
        }

        public IActionResult OnPost(int course)
        {
            _context.Set<Course>()
                .Include(c => c.CourseEnrollments)
                .FirstOrDefault(c => c.Id == course)
                .CourseEnrollments.Add(new CourseEnrollment() { EnrollmentDate = DateTime.Now, Student = new UserSession(_context).GetUserSession() });

            _context.SaveChanges();
            
            var testcourse = _context.Set<Course>()
                .Include(c => c.CourseEnrollments)
                .FirstOrDefault(c => c.Id == course);

            var user = _context.Set<User>()
                .Include(c => c.CourseEnrollments)
                .FirstOrDefault(u => u.Id == testcourse.CourseEnrollments.Select(c => c.Student).FirstOrDefault().Id);

            return RedirectToPage("Index");
        }
    }
}
