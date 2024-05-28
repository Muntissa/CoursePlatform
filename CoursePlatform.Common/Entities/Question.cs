using CoursePlatform.Common.Enums;

namespace CoursePlatform.Common.Entities
{
    public class Question : BaseEntity
    {
        public string Content { get; set; }

        public long? TestId { get; set; }
        public Test? Test { get; set; }

        public List<Answer> Answers { get; set; } = new();
    }
}
