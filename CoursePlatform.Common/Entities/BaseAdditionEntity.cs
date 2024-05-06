using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Entities
{
    public abstract class BaseAdditionEntity : BaseEntity
    {
        public virtual Lecture Lecture { get; set; }
    }
}
