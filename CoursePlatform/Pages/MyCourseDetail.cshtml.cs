using CoursePlatform.Common;
using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.InteropServices;

namespace CoursePlatform.Pages
{
    public class MyCourseDetailModel : PageModel
    {
        private readonly CoursePlatformContext _context;
        private readonly UserManager<User> _userManager;

        public MyCourseDetailModel(CoursePlatformContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Course CurrentCourse { get; set; }
        public List<Category> AllCategories { get; set; }

        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public Complexity Complexity { get; set; }
        [BindProperty]
        public List<long> SelectedCategoryIds { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? courseid)
        {
            var user = await _userManager.GetUserAsync(User);

            if (courseid == null)
                return NotFound("Course ID не предоставлен");

            if (!User.Identity.IsAuthenticated)
                return NotFound($"Вы не можете редактировать курс, не авторизировавшись");


            if (User.Identity.IsAuthenticated && !User.IsInRole("Teacher"))
                return NotFound("Только с ролью \"Teacher\" вы можете редактировать курс");

            CurrentCourse = await _context.Set<Course>()
                .Include(c => c.Teacher).ThenInclude(t => t.Profile)
                .Include(c => c.CourseEnrollments).ThenInclude(ce => ce.Student).ThenInclude(s => s.Profile)
                .Include(c => c.Lectures).ThenInclude(l => l.Progreses)
                .Include(c => c.CourseCategories)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(c => c.Id == courseid);

            if (CurrentCourse == null)
                return NotFound($"Курс с ID {courseid} не найден.");

            if (user.Id != CurrentCourse.Teacher.Id)
                return NotFound($"Вы не можете редактировать не свой курс");

            AllCategories = await _context.Set<Category>().ToListAsync();
            
            Title = CurrentCourse.CourseTitle;
            Description = CurrentCourse.CourseDecription;
            Complexity = CurrentCourse.Complexity;
            SelectedCategoryIds = CurrentCourse.CourseCategories.Select(cc => cc.Id).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteEnrollmentAsync(int enrollmentId, int? courseid)
        {
            var enrollmentToDelete = await _context.Set<CourseEnrollment>()
                .Include(ce => ce.Progreses)
                .Include(ce => ce.Certificate)  
                .FirstOrDefaultAsync(ce => ce.Id == enrollmentId);

            if (enrollmentToDelete != null)
            {
                foreach (var l in enrollmentToDelete.Progreses) 
                {
                    l.Lecture = null;
                }

                _context.Set<Progress>().RemoveRange(enrollmentToDelete.Progreses);
                _context.Set<Certificate>().Remove(enrollmentToDelete.Certificate);

                _context.Set<CourseEnrollment>().Remove(enrollmentToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(new { courseid = courseid});
        }

        public async Task<IActionResult> OnPostEditCourseAsync(int courseId)
        {
            var course = await _context.Set<Course>()
                .Include(c => c.CourseCategories)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course != null)
            {
                course.CourseTitle = Title;
                course.CourseDecription = Description;
                course.Complexity = Complexity;

                // Update categories
                course.CourseCategories.Clear();
                var selectedCategories = await _context.Set<Category>()
                    .Where(c => SelectedCategoryIds.Contains(c.Id))
                    .ToListAsync();
                course.CourseCategories.AddRange(selectedCategories);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage(new { courseid = courseId });
        }
    }

}
