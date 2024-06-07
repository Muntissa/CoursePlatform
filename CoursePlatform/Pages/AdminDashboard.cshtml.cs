using CoursePlatform.Common.Entities;
using CoursePlatform.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using CoursePlatform.Common.Migrations;

namespace CoursePlatform.Pages
{
    public class AdminDashboard : PageModel
    {
        private readonly CoursePlatformContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AdminDashboard(CoursePlatformContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        public List<Category> Categories { get; set; }
        public List<Course> Courses { get; set; }
        public List<User> Users { get; set; }

        public string? CurrentFilterType { get; set; }

        public async Task<IActionResult> OnGet(string FilterType)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
                return NotFound("Авторизируйтесь как администиратор, чтобы получить доступ к данной странице");

            CurrentFilterType = FilterType;

            if (String.IsNullOrEmpty(FilterType) || FilterType == "Courses")
                Courses = await _context.Set<Course>()
                .Include(c => c.CourseEnrollments).ThenInclude(ce => ce.Progreses)
                .Include(c => c.CourseEnrollments).ThenInclude(ce => ce.Certificate)
                .Include(c => c.Lectures).ThenInclude(l => l.AdditionalFile)
                .Include(c => c.Lectures).ThenInclude(l => l.Image)
                .Include(c => c.Lectures).ThenInclude(l => l.LectureMaterial)
                .Include(c => c.Lectures).ThenInclude(l => l.Video)
                .Include(c => c.Lectures).ThenInclude(l => l.Test).ThenInclude(t => t.Questions).ThenInclude(q => q.Answers)
                .Include(c => c.Teacher).ThenInclude(t => t.Profile)
                .ToListAsync();

            if (FilterType == "Users")
            {
                var adminRole = await _roleManager.FindByNameAsync("Admin");
                var adminUsersIds = await _context.UserRoles
                                                   .Where(ur => ur.RoleId == adminRole.Id)
                                                   .Select(ur => ur.UserId)
                                                   .ToListAsync();

                Users = await _context.Set<User>()
                    .Include(u => u.Profile)
                    .Include(u => u.CourseEnrollments)
                    .Include(u => u.Courses)
                    .Where(u => !adminUsersIds.Contains(u.Id))
                    .ToListAsync();
            }

            if (FilterType == "Categories")
                Categories = await _context.Set<Category>().ToListAsync();

            

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(int? userid)
        {
            var userRole = await _userManager.FindByIdAsync(userid.ToString());
            var roles = await _userManager.GetRolesAsync(userRole);
            var firstRole = roles.FirstOrDefault();

            if(firstRole == "Teacher")
            {
                var user = await _context.Set<User>()
                    .Include(u => u.Profile)
                    .Include(c => c.Courses).ThenInclude(c => c.Lectures).ThenInclude(l => l.LectureMaterial)
                    .Include(c => c.Courses).ThenInclude(c => c.Lectures).ThenInclude(l => l.Image)
                    .Include(c => c.Courses).ThenInclude(c => c.Lectures).ThenInclude(l => l.AdditionalFile)
                    .Include(c => c.Courses).ThenInclude(c => c.Lectures).ThenInclude(l => l.Progreses)
                    .Include(c => c.Courses).ThenInclude(c => c.Lectures).ThenInclude(l => l.Video)
                    .Include(c => c.Courses).ThenInclude(c => c.Lectures).ThenInclude(l => l.Test).ThenInclude(t => t.Questions).ThenInclude(q => q.Answers)
                    .Include(c => c.Courses).ThenInclude(c => c.CourseEnrollments).ThenInclude(c => c.Certificate)
                    .Include(c => c.Courses).ThenInclude(c => c.CourseEnrollments).ThenInclude(c => c.Progreses)
                    .FirstOrDefaultAsync(u => u.Id == userRole.Id);

                foreach(var course in user.Courses)
                {
                    foreach (var ce in course.CourseEnrollments)
                    {
                        if (ce.Certificate is not null)
                            _context.Remove(ce.Certificate);

                        _context.RemoveRange(ce.Progreses);

                        _context.Remove(ce);
                    }

                    if (course.Lectures.Count() != 0)
                    {
                        foreach (var lecture in course.Lectures)
                        {
                            if (lecture.AdditionalFile is not null)
                                _context.Remove(lecture.AdditionalFile);

                            if (lecture.Image is not null)
                                _context.Remove(lecture.Image);

                            if (lecture.Video is not null)
                                _context.Remove(lecture.Video);

                            if (lecture.LectureMaterial is not null)
                                _context.Remove(lecture.LectureMaterial);

                            if (lecture.Test is not null)
                            {
                                foreach (var question in lecture.Test.Questions)
                                    if (question.Answers.Count() != 0)
                                        _context.RemoveRange(question.Answers);
                                    
                                _context.RemoveRange(lecture.Test.Questions);

                                _context.Remove(lecture.Test);
                            }

                        }
                    }
                }
                _context.RemoveRange(user.Courses);

                _context.Remove(user.Profile);

                _context.Remove(user);

                await _context.SaveChangesAsync();
            }
            else if(firstRole == "Student")
            {
                var user = await _context.Set<User>()
                    .Include(u => u.Profile)
                    .Include(c => c.CourseEnrollments).ThenInclude(c => c.Certificate)
                    .Include(c => c.CourseEnrollments).ThenInclude(c => c.Progreses)
                    .FirstOrDefaultAsync(u => u.Id == userRole.Id);

                foreach(var ce in user.CourseEnrollments)
                {
                    if (ce.Certificate is not null)
                        _context.Remove(ce.Certificate);

                    _context.RemoveRange(ce.Progreses);

                    _context.Remove(ce);
                }

                _context.RemoveRange(user.CourseEnrollments);
                _context.Remove(user.Profile);
                _context.Remove(user);

                _context.SaveChanges();
            }


            return RedirectToPage("/AdminDashboard", new { FilterType = "Users" });
        }

        public async Task<IActionResult> OnPostDeleteCourseAsync(int? courseid)
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

            if (course.CourseEnrollments.Count() != 0)
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

            if (course.Lectures.Count() != 0)
            {
                foreach (var lecture in course.Lectures)
                {
                    if (lecture.AdditionalFile is not null)
                        _context.Remove(lecture.AdditionalFile);

                    if (lecture.Image is not null)
                        _context.Remove(lecture.Image);

                    if (lecture.Video is not null)
                        _context.Remove(lecture.Video);

                    if (lecture.LectureMaterial is not null)
                        _context.Remove(lecture.LectureMaterial);

                    if (lecture.Test is not null)
                    {
                        foreach (var question in lecture.Test.Questions)
                        {
                            if (question.Answers.Count() != 0)
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

            Courses = await _context.Set<Course>()
                .Include(c => c.CourseEnrollments).ThenInclude(ce => ce.Progreses)
                .Include(c => c.CourseEnrollments).ThenInclude(ce => ce.Certificate)
                .Include(c => c.Lectures).ThenInclude(l => l.AdditionalFile)
                .Include(c => c.Lectures).ThenInclude(l => l.Image)
                .Include(c => c.Lectures).ThenInclude(l => l.LectureMaterial)
                .Include(c => c.Lectures).ThenInclude(l => l.Video)
                .Include(c => c.Lectures).ThenInclude(l => l.Test).ThenInclude(t => t.Questions).ThenInclude(q => q.Answers)
                .Include(c => c.Teacher).ThenInclude(t => t.Profile)
                .ToListAsync();

            return RedirectToPage("/AdminDashboard", new { FilterType = "Courses" });
        }

        public async Task<IActionResult> OnPostDeleteCategoryAsync(int? categoryid)
        {
            var category = await _context.Set<Category>().FirstOrDefaultAsync(c => c.Id == categoryid);

            _context.Remove(category);

            await _context.SaveChangesAsync();

            Categories = await _context.Set<Category>().ToListAsync();

            return RedirectToPage("/AdminDashboard", new { FilterType = "Category" }); 
        }
    }

}
