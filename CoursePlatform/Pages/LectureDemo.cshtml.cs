using CoursePlatform.Common;
using CoursePlatform.Common.Additional;
using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Enums;
using CoursePlatform.Common.Migrations;
using CoursePlatform.WebApi.TempModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace CoursePlatform.Pages
{
    public class LectureDemo : PageModel
    {
        private readonly CoursePlatformContext _context;
        private readonly UserManager<User> _userManager;

        public LectureDemo(CoursePlatformContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public List<TestAnswerModel> Answers { get; set; }
        public Course Course { get; set; }
        public Lecture CurrentLecture { get; set; }
        public CourseEnrollment CurrentCE { get; set; }

        public async Task OnGetAsync(int? courseid, int? lectureid)
        {
            CurrentLecture = _context.Set<Lecture>()
                .Include(l => l.LectureMaterial)
                .Include(l => l.Test).ThenInclude(t => t.Questions).ThenInclude(q => q.Answers)
                .Include(l => l.Image)
                .Include(l => l.Video)
                .Include(l => l.AdditionalFile)
                .FirstOrDefault(c => c.Id == lectureid); ;
        }

        public async Task<IActionResult> OnPostCheckProgress(int? courseid, int? lectureid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            CurrentLecture =  _context.Set<Lecture>()
                .Include(l => l.LectureMaterial)
                .Include(l => l.Test).ThenInclude(t => t.Questions).ThenInclude(q => q.Answers)
                .Include(l => l.Image)
                .Include(l => l.Video)
                .Include(l => l.AdditionalFile)
                .FirstOrDefault(c => c.Id == lectureid); ;


            // Перенаправление или отображение результата
            return Page();
        }

        public async Task<IActionResult> OnPost(int courseid)
        {
            var user = await _userManager.GetUserAsync(User);

            _context.Set<Course>()
                .Include(c => c.CourseEnrollments)
                .FirstOrDefault(c => c.Id == courseid)
                .CourseEnrollments.Add(new CourseEnrollment() { EnrollmentDate = DateTime.Now, Student = user, Course = _context.Set<Course>().FirstOrDefault(c => c.Id == courseid) });

            _context.SaveChanges();

            return RedirectToPage(new { courseid });
        }
    }
}
