namespace CoursePlatform.Common.Entities
{
    public class Test : BaseEntity
    {
        public long? CourseId { get; set; }
        public virtual Course? Course { get; set; }

        public virtual Lecture? Lecture { get; set; } 
        public virtual List<Question> Questions { get; set; } = new();
    }
}
