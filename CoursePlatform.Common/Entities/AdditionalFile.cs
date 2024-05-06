using CoursePlatform.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Entities
{
    public class AdditionalFile : BaseAdditionEntity
    {
        public string FilePath { get; set; }
        public FileType FileType { get; set; }
    }
}
