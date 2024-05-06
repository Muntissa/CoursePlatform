using CoursePlatform.Common.Enums;

namespace CoursePlatform.Common.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Role? Role { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual List<CourseEnrollment>? CourseEnrollments { get; set; } = new();
        public virtual List<Course>? Courses { get; set; } = new();
    }
}
