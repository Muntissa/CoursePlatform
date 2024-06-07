using CoursePlatform.Common;
using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CoursePlatform.Pages
{
    public class NewCourse : PageModel
    {
        private readonly CoursePlatformContext _context;
        private readonly UserManager<User> _userManager;

        public NewCourse(CoursePlatformContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public string CategoryName { get; set; }

        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public List<int> CategoriesIds { get; set; }
        [BindProperty]
        public Complexity Complexity { get; set; }

        public List<Category> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
                return NotFound("Авторизируйтесь как \"TEACHER\", прежде чем создавать свой курс");

            if (User.Identity.IsAuthenticated && !User.IsInRole("Teacher"))
                return NotFound("Только \"TEACHER\" может создавать свой курс");

            Categories = await _context.Set<Category>().ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAddNewCategoryAsync()
        {
            if (!string.IsNullOrWhiteSpace(CategoryName))
            {
                var newCategory = new Category { Name = CategoryName };
                await _context.Set<Category>().AddAsync(newCategory);
                await _context.SaveChangesAsync();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userTeacher = await _userManager.GetUserAsync(User);

            var newCourse = new Course()
            {
                CourseTitle = Name,
                CourseDecription = Description,
                Teacher = userTeacher,
                Complexity = Complexity
            };

            var selectedCategories = await _context.Set<Category>()
                .Where(c => CategoriesIds.Contains((int)c.Id))
                .ToListAsync();

            newCourse.CourseCategories = selectedCategories;
            newCourse.Lectures.Add(new Lecture() { OrderInCourse = 1, Title = "Название лекции", SubTitle = "Тут будет краткая информация о курсе, которая будет видна в карточке", Summary = "Тут будет описание, для чего нужен этот курс", });
            _context.Set<Course>().Add(newCourse);
            await _context.SaveChangesAsync();

            return RedirectToPage("/MyCourses");
        }
    }
}
