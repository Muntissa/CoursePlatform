using CoursePlatform.Common;
using CoursePlatform.Common.Additional;
using CoursePlatform.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Pages
{
    public class Authorization : PageModel
    {
        private readonly CoursePlatformContext _context;

        public Authorization(CoursePlatformContext context)
        {
            _context = context;
        }

        public void OnPost(string name, string password) 
        {
            var user = new User { Username = name, Password = password };

            _context.Set<User>().Add(user);
            _context.SaveChanges();

        }


    }
}
