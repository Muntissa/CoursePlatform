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
        public Status CompletionStatus { get; set; }
        public int? Score { get; set; } = 0;
        public int? Total { get; set; }
        
        public virtual Lecture Lecture { get; set; }
        public virtual CourseEnrollment Enrollment { get; set; } 
    }
}
