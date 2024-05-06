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
        public virtual Course Course { get; set; }
        public long? ProgressId { get; set; }
        public virtual Progress? Progress { get; set; }

        public virtual List<Test>? Tests { get; set; }
        public virtual List<Video>? Videos { get; set; }
        public virtual List<AdditionalFile>? AdditionalFiles { get; set; }
        public virtual List<LectureMaterial>? LectureMaterials { get; set; }
    }
}
