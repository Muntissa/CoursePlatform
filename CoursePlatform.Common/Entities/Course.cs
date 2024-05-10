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
        public User? Teacher { get; set; }
        public Test? FinalTest { get; set; }
            
        public List<Category>? CourseCategories { get; set; } = new();
        public List<CourseEnrollment>? CourseEnrollments { get; set; } = new();
        public List<Lecture> Lectures { get; set; } = new();
    }
}
