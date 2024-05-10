using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Entities
{
    public class Lecture : BaseEntity
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public int OrderInCourse { get; set; }

        public long? CourseId { get; set; }
        public Course Course { get; set; }
        public long? ProgressId { get; set; }
        public Progress? Progress { get; set; }

        public Test? Tests { get; set; }
        public List<Video>? Videos { get; set; }
        public List<AdditionalFile>? AdditionalFiles { get; set; }
        public List<LectureMaterial>? LectureMaterials { get; set; }
    }
}
