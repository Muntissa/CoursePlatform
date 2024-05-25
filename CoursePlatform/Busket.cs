using CoursePlatform.Common;
using CoursePlatform.Common.Entities;

namespace CoursePlatform.WebApi
{
    public static class Busket
    {
        public static void FillDataBase(IServiceProvider provider)
        {
            using (var context = new CoursePlatformContext(provider))
            {
                context.AddRange(
                    new Role()
                    {
                        Name = "Student",
                        NormalizedName = "Студент"
                    },
                    new Role()
                    {
                        Name = "Teacher",
                        NormalizedName = "Преподаватель"
                    },
                    new Role()
                    {
                        Name = "Admin",
                        NormalizedName = "Администратор"
                    });

                context.SaveChanges();
            }
        }

    }
}
