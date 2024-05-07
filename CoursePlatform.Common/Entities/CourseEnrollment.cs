using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Entities
{
    public class CourseEnrollment : BaseEntity
    {
        public DateTime EnrollmentDate { get; set; }

        public User Student { get; set; }
        public Course Course { get; set; }
        public long? ProgressId { get; set; }
        public Progress Progress { get; set; }

        public Certificate? Certificate { get; set; }
    }
}
