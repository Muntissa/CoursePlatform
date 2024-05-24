using CoursePlatform.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Additional
{
    public static class CourseEnrollmentExt
    {
        public static int GetProgresson(this CourseEnrollment courseE)
        {
            var allProgress = courseE.Progreses.Count();

            if (allProgress == 0)
                return 0;

            var getCurrentProgress = courseE.Progreses.Where(p => p.CompletionStatus == Enums.Status.Success).Count();

            var finishProgress = allProgress / getCurrentProgress;

            return finishProgress * 100;
        }
    }
}
