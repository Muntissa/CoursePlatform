using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CoursePlatform.Common.Enums
{
    public enum Role
    {
        [Description("Преподаватель")]
        Teacher,
        [Description("Студент")]
        Student
    }
}
