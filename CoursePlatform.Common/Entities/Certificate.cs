using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Entities
{
    public class Certificate : BaseEntity
    {
        public DateTime? IssueDate { get; set; }
        public string? Path { get; set; }

        public long? CourseEnrollmentId { get; set; }
        public CourseEnrollment CourseEnrollment { get; set; }
    }
}
