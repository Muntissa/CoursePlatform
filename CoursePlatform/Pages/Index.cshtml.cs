using CoursePlatform.Common;
using CoursePlatform.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CoursePlatformContext _context;

        public IndexModel(CoursePlatformContext context)
        {
            _context = context;
        }

        public IList<Course> Courses { get; set; }
        public IList<User> Users { get; set; }

        public void OnGet()
        {
            LoadEntity();
        }

        public IActionResult OnPost(string handler)
        {
            if(handler == "Create")
            {
                var course = new Course() { CourseTitle = "Титл курса", CourseDecription = "Описание курса" };
                var course2 = new Course() { CourseTitle = "Титл 2 курса", CourseDecription = "Описание 2 курса" };

                var user = new User() { Username = "User1", Password = "Password1" };

                user.Courses.Add(course);
                user.Courses.Add(course2);

                _context.Set<User>().Add(user);

                _context.SaveChanges(); // Перенаправление на ту же страницу после выполнения POST запроса

                var userToTest = _context.Set<User>().FirstOrDefault();
            }

            if(handler == "Delete")
            {
                var user = _context.Set<User>().FirstOrDefault();

                var userProfile = _context.Set<Profile>().FirstOrDefault(u => u.UserId == user.Id);

                _context.Set<Profile>().Remove(userProfile);

                var courses = _context.Set<Course>().Where(c => c.UserId == user.Id).ToList();

                _context.RemoveRange(courses);

                _context.Set<User>().Remove(user);

                _context.SaveChanges();
            }

            return RedirectToPage();
        }

        private void LoadEntity()
        {
            Users = _context.Set<User>().Include(c => c.Courses).ToList();
            Courses = _context.Set<Course>().Include(c => c.CourseCategories).ToList();
        }
    }
}
