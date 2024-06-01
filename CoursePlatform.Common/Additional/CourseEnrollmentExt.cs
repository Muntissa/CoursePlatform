using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Additional
{
    public static class CourseEnrollmentExt
    {
        public static int GetProgress(this CourseEnrollment courseE)
        {
            if (courseE.Course.Lectures.Count == 0) return 0;

            var completedLectures = courseE.Progreses.Count(p => p.CompletionStatus == Status.Success);
            return (completedLectures * 100) / courseE.Course.Lectures.Count;
        }
    }
}
