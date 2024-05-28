using CoursePlatform.Common;
using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.WebApi
{
    public static class Busket
    {
        public static void FillDataBase(IServiceProvider provider)
        {
            using (var context = new CoursePlatformContext(provider))
            {
                var course = context.Set<Course>()
                    .Include(c => c.Lectures).ThenInclude(l => l.LectureMaterial)
                    .Include(c => c.Lectures).ThenInclude(l => l.Test).ThenInclude(t => t.Questions).ThenInclude(q => q.Answers)
                    .Include(c => c.Lectures).ThenInclude(l => l.Image)
                    .Include(c => c.Lectures).ThenInclude(l => l.Video)
                    .Include(c => c.Lectures).ThenInclude(l => l.AdditionalFile)
                    .FirstOrDefault(c => c.Id == 24);

                course.Lectures.FirstOrDefault().LectureMaterial = new LectureMaterial() { Content = "Метод Url.Page используется для создания URL на основе имени страницы и параметров. Это позволяет избежать ошибок в написании URL вручную.Метод Url.Page используется для создания URL на основе имени страницы и параметров. Это позволяет избежать ошибок в написании URL вручную.Метод Url.Page используется для создания URL на основе имени страницы и параметров. Это позволяет избежать ошибок в написании URL вручную." };

                course.Lectures.FirstOrDefault().Test = new Test();

                var test = course.Lectures.FirstOrDefault().Test;

                test
                    .Questions.Add(new()
                    {
                        Content = "Тестовый вопрос?",
                        Answers = new()
                            {
                                new()
                                {
                                    AnswerContent = "1 ответ",
                                    AnswerType = AnswerType.Correct,
                                },
                                new()
                                {
                                    AnswerContent = "2 ответ",
                                    AnswerType = AnswerType.Incorrect,
                                },
                                new()
                                {
                                    AnswerContent = "3 ответ",
                                    AnswerType = AnswerType.Incorrect,
                                },
                                new()
                                {
                                    AnswerContent = "4 ответ",
                                    AnswerType = AnswerType.Incorrect,
                                },
                        }
                    });

                course.Lectures.FirstOrDefault().Image = new Image();

                course.Lectures.FirstOrDefault().Image.ImagePath = "/image/lesson2.jpg";

                course.Lectures.FirstOrDefault().AdditionalFile = new AdditionalFile();

                course.Lectures.FirstOrDefault().AdditionalFile.FileType = FileType.Word;
                course.Lectures.FirstOrDefault().AdditionalFile.FilePath= "/files/LB2.docx";



                context.SaveChanges();


            }
        }

    }
}
