using CoursePlatform.Common;
using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Pages
{
    public class CourseConstructor : PageModel
    {
        private readonly CoursePlatformContext _context;
        private readonly UserManager<User> _userManager;

        public CourseConstructor(CoursePlatformContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public Course CurrentCourse { get; set; }
        public Lecture CurrentLecture { get; set; }

        public void OnGet(int? courseid, int? lectureid)
        {
            if (courseid is null)
                return;

            CurrentCourse = _context.Set<Course>()
                .Include(c => c.Lectures).ThenInclude(l => l.Image)
                .Include(c => c.Lectures).ThenInclude(l => l.AdditionalFile)
                .Include(c => c.Lectures).ThenInclude(l => l.Test).ThenInclude(t => t.Questions).ThenInclude(q => q.Answers)
                .Include(c => c.Lectures).ThenInclude(l => l.Video)
                .Include(c => c.Lectures).ThenInclude(l => l.LectureMaterial)
                .FirstOrDefault(c => c.Id == courseid);

            if (lectureid is null)
                CurrentLecture = CurrentCourse.Lectures.OrderBy(l => l.OrderInCourse).FirstOrDefault();
            else
                CurrentLecture = CurrentCourse.Lectures.Where(l => l.Id == lectureid).FirstOrDefault();

        }

        public async Task<IActionResult> OnPostAddNewLectureAsync(int courseid)
        {
            var course = await _context.Set<Course>()
                .Include(c => c.Lectures)
                .FirstOrDefaultAsync(c => c.Id == courseid);

            if (course == null)
            {
                return NotFound();
            }

            var newLecture = new Lecture
            {
                Title = "Новая лекция",
                SubTitle = "Подзаголовок",
                Summary = "Описание лекции",
                OrderInCourse = course.Lectures.Count + 1
            };

            course.Lectures.Add(newLecture);
            await _context.SaveChangesAsync();

            return RedirectToPage("/CourseConstructor", new { courseid = course.Id, lectureid = newLecture.Id });
        }

        public async Task<IActionResult> OnPostEditLectureTitleAsync(int courseid, int lectureid, string title, string subtitle, string summary)
        {
            var lecture = await _context.Set<Lecture>()
                .FirstOrDefaultAsync(l => l.Id == lectureid);

            if (lecture == null)
            {
                return NotFound();
            }

            lecture.Title = title;
            lecture.SubTitle = subtitle;
            lecture.Summary = summary;
            await _context.SaveChangesAsync();

            return RedirectToPage(new { lectureid = lecture.Id });
        }

        public async Task<IActionResult> OnPostAddVideoAsync(int courseid, int lectureid, string videourl)
        {
            DeleteAnother("Video", lectureid);

            var lecture = await _context.Set<Lecture>()
                .Include(l => l.Video)
                .FirstOrDefaultAsync(l => l.Id == lectureid);

            if (lecture == null)
            {
                return NotFound();
            }

            lecture.Video = new Video{ VideoURL = videourl};
            await _context.SaveChangesAsync();

            return RedirectToPage(new { courseid = courseid, lectureid = lecture.Id });
        }

        public async Task<IActionResult> OnPostAddLectureMaterialAsync(int courseid, int lectureid, string content)
        {
            DeleteAnother("Text", lectureid);

            var lecture = await _context.Set<Lecture>()
                .Include(l => l.LectureMaterial)
                .FirstOrDefaultAsync(l => l.Id == lectureid);

            if (lecture == null)
            {
                return NotFound();
            }

            lecture.LectureMaterial = new LectureMaterial { Content = content };
            await _context.SaveChangesAsync();

            return RedirectToPage(new { courseid = courseid, lectureid = lecture.Id });
        }

        public async Task<IActionResult> OnPostAddImageAsync(int courseid, int lectureid, IFormFile imageFile)
        {
            DeleteAnother("Image", lectureid);

            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("Image file is not selected");

            var lecture = await _context.Set<Lecture>()
                .Include(l => l.Image)
                .FirstOrDefaultAsync(l => l.Id == lectureid);

            if (lecture == null)
            {
                return NotFound();
            }

            // Удаление существующего изображения, если оно есть
            if (lecture.Image != null)
            {
                var existingImagePath = Path.Combine("wwwroot/image/lectureImages", lecture.Image.ImagePath);
                if (System.IO.File.Exists(existingImagePath))
                {
                    System.IO.File.Delete(existingImagePath);
                }
            }

            // Создание директории, если её нет
            var imageDirectory = Path.Combine("wwwroot", "image", "lectureImages");
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            // Сохранение нового изображения
            var fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
            var filePath = Path.Combine(imageDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            lecture.Image = new Image { ImagePath = fileName };
            await _context.SaveChangesAsync();

            return RedirectToPage(new { courseid = courseid, lectureid = lecture.Id });
        }

        public async Task<IActionResult> OnPostAddAdditionalFileAsync(int courseid, int lectureid, IFormFile additionalFile)
        {
            DeleteAnother("AdditionalFile", lectureid);

            if (additionalFile == null || additionalFile.Length == 0)
                return BadRequest("File is not selected");

            var lecture = await _context.Set<Lecture>()
                .Include(l => l.AdditionalFile)
                .FirstOrDefaultAsync(l => l.Id == lectureid);

            if (lecture == null)
            {
                return NotFound();
            }

            // Удаление существующего файла, если он есть
            if (lecture.AdditionalFile != null)
            {
                var existingFilePath = Path.Combine("wwwroot/files/lectureFiles", lecture.AdditionalFile.FilePath);
                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath);
                }
            }

            // Создание директории, если её нет
            var filesDirectory = Path.Combine("wwwroot", "files", "lectureFiles");
            if (!Directory.Exists(filesDirectory))
            {
                Directory.CreateDirectory(filesDirectory);
            }

            // Сохранение нового файла
            var fileName = $"{Guid.NewGuid()}_{additionalFile.FileName}";
            var filePath = Path.Combine(filesDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await additionalFile.CopyToAsync(stream);
            }

            lecture.AdditionalFile = new AdditionalFile { FilePath = fileName, FileType = GetFileType(fileName) };
            await _context.SaveChangesAsync();

            return RedirectToPage(new { courseid = courseid, lectureid = lecture.Id });
        }

        private FileType GetFileType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            return extension switch
            {
                ".pdf" => FileType.PDF,
                ".doc" => FileType.Word,
                _ => throw new ArgumentOutOfRangeException("Unsupported file type")
            };
        }

        public void DeleteAnother(string type, int lectureid)
        {
            var lectureToEdit = _context.Set<Lecture>()
                .Include(l => l.Image)
                .Include(l => l.AdditionalFile)
                .Include(l => l.Test).ThenInclude(t => t.Questions).ThenInclude(q => q.Answers)
                .Include(l => l.Video)
                .Include(l => l.LectureMaterial)
                .FirstOrDefault(l => l.Id == lectureid);

            if (type == "Text")
            {
                if(lectureToEdit.Image != null)
                    _context.Remove(lectureToEdit.Image);

                if (lectureToEdit.Video != null)
                    _context.Remove(lectureToEdit.Video);

                if(lectureToEdit.Test != null)
                {
                    foreach (var q in lectureToEdit.Test.Questions)
                        _context.RemoveRange(q.Answers);
                    _context.RemoveRange(lectureToEdit.Test.Questions);

                    _context.Remove(lectureToEdit.Test);
                }

                if (lectureToEdit.AdditionalFile != null)
                    _context.Remove(lectureToEdit.AdditionalFile);
            }
            else if (type == "Image")
            {
                if (lectureToEdit.LectureMaterial != null)
                    _context.Remove(lectureToEdit.LectureMaterial);

                if (lectureToEdit.Video != null)
                    _context.Remove(lectureToEdit.Video);

                if (lectureToEdit.Test != null)
                {
                    foreach (var q in lectureToEdit.Test.Questions)
                        _context.RemoveRange(q.Answers);
                    _context.RemoveRange(lectureToEdit.Test.Questions);

                    _context.Remove(lectureToEdit.Test);
                }

                if (lectureToEdit.AdditionalFile != null)
                    _context.Remove(lectureToEdit.AdditionalFile);
            }
            else if (type == "Video")
            {
                if (lectureToEdit.Image != null)
                    _context.Remove(lectureToEdit.Image);

                if (lectureToEdit.LectureMaterial != null)
                    _context.Remove(lectureToEdit.LectureMaterial);

                if (lectureToEdit.Test != null)
                {
                    foreach (var q in lectureToEdit.Test.Questions)
                        _context.RemoveRange(q.Answers);
                    _context.RemoveRange(lectureToEdit.Test.Questions);

                    _context.Remove(lectureToEdit.Test);
                }

                if (lectureToEdit.AdditionalFile != null)
                    _context.Remove(lectureToEdit.AdditionalFile);
            }
            else if (type == "Test")
            {
                if (lectureToEdit.Image != null)
                    _context.Remove(lectureToEdit.Image);

                if (lectureToEdit.Video != null)
                    _context.Remove(lectureToEdit.Video);

                if (lectureToEdit.LectureMaterial != null)
                    _context.Remove(lectureToEdit.LectureMaterial);

                if (lectureToEdit.AdditionalFile != null)
                    _context.Remove(lectureToEdit.AdditionalFile);
            }
            else if (type == "AdditionalFile")
            {
                if (lectureToEdit.Image != null)
                    _context.Remove(lectureToEdit.Image);

                if (lectureToEdit.Video != null)
                    _context.Remove(lectureToEdit.Video);


                if (lectureToEdit.Test != null)
                {
                    foreach (var q in lectureToEdit.Test.Questions)
                        _context.RemoveRange(q.Answers);
                    _context.RemoveRange(lectureToEdit.Test.Questions);

                    _context.Remove(lectureToEdit.Test);
                }

                if (lectureToEdit.LectureMaterial != null)
                    _context.Remove(lectureToEdit.LectureMaterial);
            }

            _context.SaveChanges();
        }

    }
}
