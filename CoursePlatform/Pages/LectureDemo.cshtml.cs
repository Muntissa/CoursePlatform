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

        public async Task<IActionResult> OnGetAsync(int? courseid, int? lectureid)
        {
            if (!User.Identity.IsAuthenticated)
                return NotFound("Авторизируйтесь как \"Teacher\", чтобы просматривать данную страницу");

            CurrentLecture = _context.Set<Lecture>()
                .Include(l => l.LectureMaterial)
                .Include(l => l.Test).ThenInclude(t => t.Questions).ThenInclude(q => q.Answers)
                .Include(l => l.Image)
                .Include(l => l.Video)
                .Include(l => l.AdditionalFile)
                .Include(l => l.Course).ThenInclude(c => c.Teacher)
                .FirstOrDefault(c => c.Id == lectureid);

            var user = await _userManager.GetUserAsync(User);

            if (user.Id != CurrentLecture.Course.Teacher.Id)
                return NotFound("Мы не можете просматривать ДЕМО страницу не своей лекции");

            return Page();
        }
    }
}
