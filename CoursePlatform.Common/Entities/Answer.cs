using CoursePlatform.Common.Enums;

namespace CoursePlatform.Common.Entities
{
    public class Answer : BaseEntity
    {
        public string AnswerContent { get; set; }
        public AnswerType AnswerType { get; set; }


        public long QuestionId { get; set; }
        public Question Question { get; set; }
    }
}