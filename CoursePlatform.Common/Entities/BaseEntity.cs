using CoursePlatform.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Entities
{
    public abstract class BaseEntity : IDataEntity
    {
        public long Id { get; set; }
    }
}
