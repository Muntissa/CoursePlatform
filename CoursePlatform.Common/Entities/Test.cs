namespace CoursePlatform.Common.Entities
{
    public class Test : BaseEntity
    {
        public long? CourseId { get; set; }
        public Course? Course { get; set; }

        public Lecture? Lecture { get; set; } 
        public List<Question> Questions { get; set; } = new();
    }
}
