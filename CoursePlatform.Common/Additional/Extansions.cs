using CoursePlatform.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Additional
{
    public static class Extansions
    {
        public static string GetDescription(this Role role)
        {
            FieldInfo field = role.GetType().GetField(role.ToString());
            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? role.ToString() : attribute.Description;
        }
    }
}
