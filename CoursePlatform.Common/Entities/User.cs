using CoursePlatform.Common.Additional;
using CoursePlatform.Common.Enums;
using CoursePlatform.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace CoursePlatform.Common.Entities
{
    [Index(nameof(Username), IsUnique = true)]
    public class User : BaseEntity
    {
        public User()
        {
            if(Profile is null)
                Profile = new Profile() { Name = "", Surname = "Test", LastName = "" };
        }
        
        public string Username { get; set; }
        public string Password { get; set; }
        public Role? Role { get; set; }

        public Profile Profile { get; set; }
        public List<CourseEnrollment>? CourseEnrollments { get; set; } = new();
        public List<Course>? Courses { get; set; } = new();
    }
}
