using CoursePlatform.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Common.Additional
{
    public static class DeleteAnotherClass
    {
        public static void DeleteAnother(string type, int lectureid, CoursePlatformContext _context)
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
