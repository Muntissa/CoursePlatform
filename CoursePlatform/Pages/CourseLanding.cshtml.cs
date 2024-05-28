using CoursePlatform.Common;
using CoursePlatform.Common.Additional;
using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Enums;
using CoursePlatform.WebApi.TempModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace CoursePlatform.Pages
{
    public class CourseLanding : PageModel
    {
        private readonly CoursePlatformContext _context;
        private readonly UserManager<User> _userManager;

        public CourseLanding(CoursePlatformContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public List<TestAnswerModel> Answers { get; set; }
        public Course Course { get; set; }
        public Lecture CurrentLecture { get; set; }

        public void OnGet(int? courseid, int? lectureid)
        {
            if (courseid is null)
                return;

            Course = _context.Set<Course>()
                .Include(c => c.Lectures).ThenInclude(l => l.LectureMaterial)
                .Include(c => c.Lectures).ThenInclude(l => l.Test).ThenInclude(t => t.Questions).ThenInclude(q => q.Answers)
                .Include(c => c.Lectures).ThenInclude(l => l.Image)
                .Include(c => c.Lectures).ThenInclude(l => l.Video)
                .Include(c => c.Lectures).ThenInclude(l => l.AdditionalFile)
                .FirstOrDefault(c => c.Id == courseid);

            CurrentLecture = Course.Lectures.Where(l => l.Id == lectureid).FirstOrDefault();
        }

        public IActionResult OnPostTestCheck()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Логика проверки правильности ответов
            foreach (var answer in Answers)
            {
                // Получение вопроса и ответа из базы данных для проверки правильности
                var question = _context.Set<Question>()
                    .Include(q => q.Answers)
                    .FirstOrDefault(q => q.Id == answer.QuestionId);

                var selectedAnswer = question?.Answers.FirstOrDefault(a => a.Id == answer.SelectedAnswerId);

                if (selectedAnswer != null && selectedAnswer.AnswerType == AnswerType.Correct)
                {
                    // Ответ правильный
                }
                else
                {
                    // Ответ неправильный
                }
            }

            // Перенаправление или отображение результата
            return RedirectToPage("TestResult");
        }

        public async Task<IActionResult> OnPost(int courseid)
        {
            var user = await _userManager.GetUserAsync(User);

            _context.Set<Course>()
                .Include(c => c.CourseEnrollments)
                .FirstOrDefault(c => c.Id == courseid)
                .CourseEnrollments.Add(new CourseEnrollment() { EnrollmentDate = DateTime.Now, Student = user });

            _context.SaveChanges();

            return RedirectToPage(new { courseid });
        }
    }
}
