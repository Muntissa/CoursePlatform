using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Entities
{
    public class Course : BaseEntity
    {
        public string CourseTitle { get; set; }
        public string CourseDecription { get; set; }
        
        public long? UserId { get; set; }
        public virtual User Teacher { get; set; }
        public virtual Test? FinalTest { get; set; }
            
        public virtual List<Category> CourseCategories { get; set; } = new();
        public virtual List<CourseEnrollment> CourseEnrollments { get; set; } = new();
        public virtual List<Lecture> Lectures { get; set; } = new();
    }
}
