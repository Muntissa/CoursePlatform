using CoursePlatform.Common;
using CoursePlatform.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.WebApi
{
    public static class Busket
    {
        public static void FillDataBase(IServiceProvider provider)
        {
            using (var context = new CoursePlatformContext(provider))
            {
                var course = context.Set<Course>().Include(c => c.Lectures).FirstOrDefault(c => c.Id == 25);

                course.Lectures.Add(new Lecture() { OrderInCourse = 3, Title = "Тест", Summary = "Тест самари" });

                context.SaveChanges();
            }
        }

    }
}
