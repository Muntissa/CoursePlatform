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

        public virtual User Student { get; set; }
        public virtual Course Course { get; set; }
        public long? ProgressId { get; set; }
        public virtual Progress Progress { get; set; }

        public virtual Certificate? Certificate { get; set; }
    }
}
