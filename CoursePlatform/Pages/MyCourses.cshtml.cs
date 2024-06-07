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
    public class MyCourses : PageModel
    {
        private readonly CoursePlatformContext _context;
        private readonly UserManager<User> _userManager;

        public MyCourses(CoursePlatformContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<Course> Courses { get; set; }
        public User CurrentUser { get; set; }

        public async Task<IActionResult> OnGet(string searchTerm)
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            if (!User.Identity.IsAuthenticated)
                return NotFound("Перед тем, как перейти на данную вкадку, авторизируйтесь под ролью \"TEACHER\".");

            if (!User.IsInRole("Teacher"))
                return NotFound("Только роль \"TEACHER\" позволяет зайти на вкладку \"Мои курсы\".");

            IQueryable<Course> query = _context.Set<Course>()
                .Include(c => c.Lectures)
                .Include(c => c.Teacher).ThenInclude(t => t.Profile)
                .Include(c => c.CourseCategories)
                .Where(c => c.Teacher == CurrentUser);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var normilizeST = searchTerm.ToLower();
                query = query.Where(c => c.CourseTitle.ToLower().Contains(normilizeST) || c.CourseDecription.ToLower().Contains(normilizeST));
            }

            Courses = await query.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteMyCourseAsync(int courseid)
        {
            var course = await _context.Set<Course>()
                .Include(c => c.CourseEnrollments).ThenInclude(ce => ce.Progreses)
                .Include(c => c.CourseEnrollments).ThenInclude(ce => ce.Certificate)
                .Include(c => c.Lectures).ThenInclude(l => l.AdditionalFile)
                .Include(c => c.Lectures).ThenInclude(l => l.Image)
                .Include(c => c.Lectures).ThenInclude(l => l.LectureMaterial)
                .Include(c => c.Lectures).ThenInclude(l => l.Video)
                .Include(c => c.Lectures).ThenInclude(l => l.Test).ThenInclude(t => t.Questions).ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(c => c.Id == courseid);

            if(course.CourseEnrollments.Count() != 0)
            {
                foreach (var ce in course.CourseEnrollments)
                {
                    if (ce.Certificate is not null)
                    {
                        _context.Remove(ce.Certificate);
                    }

                    _context.RemoveRange(ce.Progreses);

                    _context.Remove(ce);
                }
            }

            if(course.Lectures.Count() != 0)
            {
                foreach(var lecture in course.Lectures)
                {
                    if(lecture.AdditionalFile is not null)
                        _context.Remove(lecture.AdditionalFile);

                    if (lecture.Image is not null)
                        _context.Remove(lecture.Image);

                    if (lecture.Video is not null)
                        _context.Remove(lecture.Video);

                    if (lecture.LectureMaterial is not null)
                        _context.Remove(lecture.LectureMaterial);

                    if(lecture.Test is not null)
                    {
                        foreach(var question in lecture.Test.Questions)
                        {
                            if(question.Answers.Count() != 0)
                            {
                                _context.RemoveRange(question.Answers);
                            }
                        }

                        _context.RemoveRange(lecture.Test.Questions);

                        _context.Remove(lecture.Test);
                    }

                }
            }

            _context.Remove(course);

            await _context.SaveChangesAsync();


            // Перенаправление на ту же страницу с параметром courseid
            return RedirectToPage("/MyCourses");
        }

        public async Task<IActionResult> OnPostAsync(int courseid)
        {
            var user = await _userManager.GetUserAsync(User);

            var course = _context.Set<Course>()
                .FirstOrDefault(c => c.Id == courseid);

            // Перенаправление на ту же страницу с параметром courseid
            return RedirectToPage("/MyCourseDetail", new { courseid });
        }

    }
}
