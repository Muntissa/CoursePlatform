namespace CoursePlatform.Common.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public List<Course>? Courses { get; set; } = new();
    }
}
