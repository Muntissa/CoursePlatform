using CoursePlatform.Common.Enums;

namespace CoursePlatform.Common.Entities
{
    public class Progress : BaseEntity
    {
        public Status? CompletionStatus { get; set; }
        public long? LectureId { get; set; }
        public Lecture? Lecture { get; set; }

        public long CourseEnrollmentId { get; set; }
        public CourseEnrollment CourseEnrollment { get; set; } 
    }
}
