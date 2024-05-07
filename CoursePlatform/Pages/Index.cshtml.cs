using CoursePlatform.Common;
using CoursePlatform.Common.Additional;
using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Interfaces;
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

        public IList<Profile> Profiles { get; set; }
        public IList<User> Users { get; set; }
        public IList<Course> Courses { get; set; }
        public string Role { get; set; }

        public async Task OnGetAsync()
        {
            LoadProfilesAsync();

            /*var user = new User() { Role = Common.Enums.Role.Student, Username = "Muntissa", Password = "admin" };

            _context.Add(user);

            _context.SaveChangesAsync();

            LoadProfilesAsync();*/
        }

        /*        public async Task<IActionResult> OnPostAsync()
                {
                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    await _serviceProvider.GetRequiredService<IContextHelper>().InContext<CoursePlatformContext>(async (context) =>
                    {
                        var user = new User(_serviceProvider) { Role = Common.Enums.Role.Student, Username = "Muntissa", Password = "admin" };

                        await context.SaveChangesAsync();
                    });

                    return RedirectToPage();
                }*/

        private void LoadProfilesAsync()
        {
            Profiles = _context.Set<Profile>().ToList();
            Users = _context.Set<User>().ToList();
        }
    }
}
