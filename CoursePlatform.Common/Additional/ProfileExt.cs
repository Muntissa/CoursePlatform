using CoursePlatform.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Additional
{

    public static class ProfileExt
    {
        public static string GetFullName(this Profile profile)
        {
            return $"{profile.Surname} {profile.Name} {profile.LastName}";
        }

        public static string GetShoryName(this Profile profile)
        {
            return $"{profile.Surname} {profile.Name} {profile.LastName}";
        }
    }
}
