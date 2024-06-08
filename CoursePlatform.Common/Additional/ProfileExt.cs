using CoursePlatform.Common.Entities;

namespace CoursePlatform.Common.Additional
{

    public static class ProfileExt
    {
        public static string GetFullName(this Profile profile)
        {
            return $"{profile.Surname} {profile.Name} {profile.LastName}";
        }
    }
}
