using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Entities
{
    public class Video : BaseAdditionEntity
    {
        public long VideoId { get; set; }
        public string VideoURL { get; set; }
    }
}
