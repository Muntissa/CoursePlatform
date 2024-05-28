using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Entities
{
    public abstract class BaseAdditionEntity : BaseEntity
    {
        public long? LectureId { get; set; }
        public Lecture Lecture { get; set; }
        
    }
}
