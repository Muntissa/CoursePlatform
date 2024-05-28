using CoursePlatform.Common;
using CoursePlatform.Common.Additional;
using CoursePlatform.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace CoursePlatform.Pages
{
    public class CourseConstructor : PageModel
    {
        private readonly CoursePlatformContext _context;
        private readonly UserManager<User> _userManager;

        public CourseConstructor(CoursePlatformContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public Course Course { get; set; }
        public Lecture CurrentLecture { get; set; }

        public void OnGet(int? courseid, int? lectureid)
        {
            if (courseid is null)
                return;

            Course = _context.Set<Course>()
                .Include(c => c.Lectures)
                .FirstOrDefault(c => c.Id == courseid);

            CurrentLecture = Course.Lectures.Where(l => l.Id == lectureid).FirstOrDefault();
        }

        public async Task<IActionResult> OnPost(int courseid)
        {
            var user = await _userManager.GetUserAsync(User);

            _context.Set<Course>()
                .Include(c => c.CourseEnrollments)
                .FirstOrDefault(c => c.Id == courseid)
                .CourseEnrollments.Add(new CourseEnrollment() { EnrollmentDate = DateTime.Now, Student = user });

            _context.SaveChanges();

            return RedirectToPage(new { courseid });
        }
    }
}
