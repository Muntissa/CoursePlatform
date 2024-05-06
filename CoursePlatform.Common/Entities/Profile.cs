using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Entities
{
    public class Profile : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }

        public long? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
