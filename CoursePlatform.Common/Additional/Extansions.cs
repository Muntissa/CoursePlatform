using System.Text.RegularExpressions;

namespace CoursePlatform.Common.Additional
{
    public static class Extansions
    {
        public static string RemoveSpecAndNum(this string inputString)
        {
            string pattern = "[^a-zA-Zа-яА-Я]";
            // Замена найденных символов на пустую строку
            string result = Regex.Replace(inputString, pattern, "");
            return result;
        }
    }
}
