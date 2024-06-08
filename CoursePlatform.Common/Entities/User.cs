using Microsoft.AspNetCore.Identity;

namespace CoursePlatform.Common.Entities
{
    public class User : IdentityUser<long>
    {
        public User()
        {
            if(Profile is null)
                Profile = new Profile() { Name = "Имя", Surname = "Фамилия", LastName = "Отчество" };
        }
        

        public Profile Profile { get; set; }
        public List<CourseEnrollment>? CourseEnrollments { get; set; } = new();
        public List<Course>? Courses { get; set; } = new();
    }
}
