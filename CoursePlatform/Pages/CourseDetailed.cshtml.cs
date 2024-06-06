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

        public async Task<IActionResult> OnGet(int? courseid)
        {
            Course = _context.Set<Course>()
                .Include(c => c.Lectures).ThenInclude(l => l.LectureMaterial)
                .Include(c => c.Lectures).ThenInclude(l => l.Image)
                .Include(c => c.Lectures).ThenInclude(l => l.Video)
                .Include(c => c.Lectures).ThenInclude(l => l.AdditionalFile)
                .Include(c => c.Lectures).ThenInclude(l => l.Test)
                .Include(c => c.Teacher)
                .ThenInclude(t => t.Profile)
                .FirstOrDefault(c => c.Id == courseid);

            if (Course is null)
                return NotFound("Данный курс не найден");

            return Page();
        }

        [HttpPost]
        public async Task<IActionResult> OnPost(int courseid)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            _context.Set<User>()
                .Include(c => c.CourseEnrollments)
                .FirstOrDefault(c => c.Id == currentUser.Id)
                .CourseEnrollments.Add(new CourseEnrollment() 
                { 
                    EnrollmentDate = DateTime.Now,
                    Course = _context.Set<Course>().FirstOrDefault(c => c.Id == courseid),
                    Certificate = new() { Path = $"/image/Student{currentUser.UserName}{Course.CourseTitle.Replace(" ", "")}" },
                });


            _context.SaveChanges();

            var userToTest = _context.Set<User>()
                .Include(u => u.CourseEnrollments)
                .ThenInclude(ce => ce.Certificate)
                .FirstOrDefault(u => u.Id == currentUser.Id);
            /*            
                        var testcourse = _context.Set<Course>()
                            .Include(c => c.CourseEnrollments)
                            .FirstOrDefault(c => c.Id == course);

                        var user = _context.Set<User>()
                            .Include(c => c.CourseEnrollments)
                            .FirstOrDefault(u => u.Id == testcourse.CourseEnrollments.Select(c => c.Student).FirstOrDefault().Id);*/

            return RedirectToPage("/Index");
        }
    }
}
