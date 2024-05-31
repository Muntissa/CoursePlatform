using CoursePlatform.Common;
using CoursePlatform.Common.Additional;
using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Enums;
using CoursePlatform.Common.Migrations;
using CoursePlatform.WebApi.TempModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace CoursePlatform.Pages
{
    public class TestCreate : PageModel
    {
        private readonly CoursePlatformContext _context;
        private readonly UserManager<User> _userManager;

        public TestCreate(CoursePlatformContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public List<TestAnswerModel> Answers { get; set; }
        public Course Course { get; set; }
        public Lecture CurrentLecture { get; set; }
        public CourseEnrollment CurrentCE { get; set; }

        public async Task OnGetAsync(int? courseid, int? lectureid)
        {
            if (courseid is null)
                return;

            Course = _context.Set<Course>()
                .Include(c => c.Lectures).ThenInclude(l => l.LectureMaterial)
                .Include(c => c.CourseEnrollments)
                .Include(c => c.Lectures).ThenInclude(l => l.Test).ThenInclude(t => t.Questions).ThenInclude(q => q.Answers)
                .Include(c => c.Lectures).ThenInclude(l => l.Image)
                .Include(c => c.Lectures).ThenInclude(l => l.Video)
                .Include(c => c.Lectures).ThenInclude(l => l.AdditionalFile)
                .FirstOrDefault(c => c.Id == courseid);

            var user = await _userManager.GetUserAsync(User);

            var studentsCEs = _context.Set<CourseEnrollment>().Include(c => c.Progreses).Include(ce => ce.Course).Where(ce => ce.StudentId == user.Id).ToList();

            CurrentCE = studentsCEs.FirstOrDefault(ce => ce.Course.Id == courseid);

            CurrentLecture = Course.Lectures.Where(l => l.Id == lectureid).FirstOrDefault();
        }

        public async Task<IActionResult> OnPostCheckProgress(int? courseid, int? lectureid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Course = _context.Set<Course>()
                .Include(c => c.Lectures).ThenInclude(l => l.LectureMaterial)
                .Include(c => c.CourseEnrollments)
                .Include(c => c.Lectures).ThenInclude(l => l.Test).ThenInclude(t => t.Questions).ThenInclude(q => q.Answers)
                .Include(c => c.Lectures).ThenInclude(l => l.Image)
                .Include(c => c.Lectures).ThenInclude(l => l.Video)
                .Include(c => c.Lectures).ThenInclude(l => l.AdditionalFile)
                .FirstOrDefault(c => c.Id == courseid);

            CurrentLecture = Course.Lectures.Where(l => l.Id == lectureid).FirstOrDefault();

            if (Answers.Count() != 0)
            {
                var counter = 0;

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
                        counter++;
                        continue;
                    }
                        
                    else
                        return Page();
                    
                }

                var lecture = CurrentLecture;

                if (counter == lecture.Test.Questions.Count())
                {
                    var user = await _userManager.GetUserAsync(User);

                    CurrentCE = _context.Set<CourseEnrollment>().FirstOrDefault(ce => ce.StudentId == user.Id);

                    CurrentCE.Progreses.Add(new() { Lecture = CurrentLecture, CompletionStatus = Status.Success });

                    _context.SaveChanges();
                }
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);

                CurrentCE = _context.Set<CourseEnrollment>().FirstOrDefault(ce => ce.StudentId == user.Id);

                CurrentCE.Progreses.Add(new() { Lecture = CurrentLecture, CompletionStatus = Status.Success });

                _context.SaveChanges();
            }


            // Перенаправление или отображение результата
            return RedirectToPage("/CourseLanding", new { courseid = courseid, lectureid = lectureid });
        }

        public async Task<IActionResult> OnPost(int courseid)
        {
            var user = await _userManager.GetUserAsync(User);

            _context.Set<Course>()
                .Include(c => c.CourseEnrollments)
                .FirstOrDefault(c => c.Id == courseid)
                .CourseEnrollments.Add(new CourseEnrollment() { EnrollmentDate = DateTime.Now, Student = user, Course = _context.Set<Course>().FirstOrDefault(c => c.Id == courseid) });

            _context.SaveChanges();

            return RedirectToPage(new { courseid });
        }
    }
}
