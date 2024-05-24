using CoursePlatform.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Entities
{
    public class Progress : BaseEntity
    {
        public Status? CompletionStatus { get; set; }
        
        public Lecture Lecture { get; set; }

        public long CourseEnrollmentId { get; set; }
        public CourseEnrollment Enrollment { get; set; } 
    }
}
