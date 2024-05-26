using CoursePlatform.Common.Additional;

namespace CoursePlatform.Common.Entities
{
    public class CourseEnrollment : BaseEntity
    {
        public DateTime EnrollmentDate { get; set; }


        public long StudentId { get; set; }
        public User Student { get; set; }

        public long CourseId { get; set; }
        public Course Course { get; set; }

        public List<Progress>? Progreses { get; set; } = new();
        public Certificate? Certificate { get; set; }
    }
}
