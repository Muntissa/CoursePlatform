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


        public int counter = 1;
        public Test CurrentTest { get; set; }
        public Question CurrentQuestion { get; set; }

        public async Task<IActionResult> OnGetAsync(int? testid, int? questionid)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Teacher"))
                return NotFound("Авторизируйтесь как \"TEACHER\", чтобы создавать тесты");

            var test = await _context.Set<Test>()
                .Include(t => t.Lecture)
                .Include(t => t.Questions).ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(l => l.Id == testid);

            if (testid is null)
                return NotFound($"Тест с ID {testid} не найден");

            CurrentTest = test;

            if(questionid is null)
                CurrentQuestion = test.Questions.FirstOrDefault();
            else
                CurrentQuestion = test.Questions.FirstOrDefault(q => q.Id == questionid);

            return Page();
        }

        public async Task<IActionResult> OnPost(int lectureid)
        {
            var lecture = await _context.Set<Lecture>()
                .Include(l => l.Test)
                .FirstOrDefaultAsync(l => l.Id == lectureid);

            DeleteAnotherClass.DeleteAnother("Test", lectureid, _context);

            if(lecture.Test is null)
                lecture.Test = new Test();
            
            _context.SaveChanges();

            return RedirectToPage("/TestCreate", new { testid = lecture.Test.Id});
        }

        public async Task<IActionResult> OnPostDeleteQuestionAsync(int? testid, int? questionid)
        {
            var test = await _context.Set<Test>()
                .Include(t => t.Questions).ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(t => t.Id == testid);

            var question = test.Questions.FirstOrDefault(q => q.Id == questionid);

            if(question is not null)
            {
                foreach (var answer in question.Answers)
                    _context.Remove(answer);

                _context.Remove(question);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/TestCreate", new { testid = testid });
        }

        public async Task<IActionResult> OnPostAddNewQuestionAsync(int testid)
        {
            var test = await _context.Set<Test>()
                .Include(t => t.Questions).ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(c => c.Id == testid);

            if (test == null)
            {
                return NotFound();
            }

            var newAnswers = new List<Answer>()
            {
                new()
                {
                    AnswerContent = "Вариант А",
                    AnswerType = AnswerType.Correct
                },
                new()
                {
                    AnswerContent = "Вариант B",
                    AnswerType = AnswerType.Incorrect
                },
                new()
                {
                    AnswerContent = "Вариант А",
                    AnswerType = AnswerType.Incorrect
                },
                new()
                {
                    AnswerContent = "Вариант А",
                    AnswerType = AnswerType.Incorrect
                },
            };

            var newQuestion = new Question() { Content = "Новый вопрос?", Answers = newAnswers };

            test.Questions.Add(newQuestion);

            await _context.SaveChangesAsync();

            return RedirectToPage(new { testid = test.Id });
        }

        public async Task<IActionResult> OnPostSaveQuestionAsync(int questionid, string questionContent, int correctAnswerIndex, string[] answers)
        {
            var question = await _context.Set<Question>().Include(q => q.Answers).FirstOrDefaultAsync(q => q.Id == questionid);

            if (question == null)
            {
                return NotFound();
            }

            question.Content = questionContent;
            for (int i = 0; i < question.Answers.Count; i++)
            {
                question.Answers[i].AnswerContent = answers[i];
                question.Answers[i].AnswerType = (i == correctAnswerIndex) ? AnswerType.Correct : AnswerType.Incorrect;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage(new { testid = question.TestId, questionid = question.Id });
        }
    }
}
