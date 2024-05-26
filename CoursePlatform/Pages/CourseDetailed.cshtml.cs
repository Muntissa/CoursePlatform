using CoursePlatform.Common;
using CoursePlatform.Common.Additional;
using CoursePlatform.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Pages
{
    public class CourseDetailedModel : PageModel
    {
        private readonly CoursePlatformContext _context;
        private readonly UserManager<User> _userManager;

        public CourseDetailedModel(CoursePlatformContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Course Course { get; set; }

        public void OnGet(int? id)
        {
            if (id is null)
                return;

            Course = _context.Set<Course>().Where(c => c.Id == id)
                .Include(c => c.Lectures)
                .Include(c => c.Teacher)
                .ThenInclude(t => t.Profile)
                .FirstOrDefault();
        }

        public async Task<IActionResult> OnPost(int course)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            _context.Set<User>()
                .Include(c => c.CourseEnrollments)
                .FirstOrDefault(c => c.Id == currentUser.Id)
                .CourseEnrollments.Add(new CourseEnrollment() 
                { 
                    EnrollmentDate = DateTime.Now, 
                    Certificate = new() { Path = $"/image/Student{currentUser.UserName}{Course.CourseTitle.Replace(" ", "")}" },
                    Progreses = new(Course.Lectures.Count())
                });

            _context.SaveChanges();
            
            var testcourse = _context.Set<Course>()
                .Include(c => c.CourseEnrollments)
                .FirstOrDefault(c => c.Id == course);

            var user = _context.Set<User>()
                .Include(c => c.CourseEnrollments)
                .FirstOrDefault(u => u.Id == testcourse.CourseEnrollments.Select(c => c.Student).FirstOrDefault().Id);

            return RedirectToPage("/Index");
        }
    }
}
