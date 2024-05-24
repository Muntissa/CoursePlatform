using CoursePlatform.Common.Additional;

namespace CoursePlatform.Common.Entities
{
    public class CourseEnrollment : BaseEntity
    {
        public DateTime EnrollmentDate { get; set; }

        public User Student { get; set; }
        public Course Course { get; set; }
        public List<Progress> Progreses { get; set; }
        public Certificate? Certificate { get; set; }
    }
}
