using CoursePlatform.Common.Enums;

namespace CoursePlatform.Common.Entities
{
    public class Question : BaseEntity
    {
        public string Content { get; set; }
        public QuestionType QuestionType { get; set; }
        public string? ImagePath { get; set; }

        public Test Test { get; set; }
    }
}
