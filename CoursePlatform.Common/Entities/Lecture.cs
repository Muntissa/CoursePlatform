namespace CoursePlatform.Common.Entities
{
    public class Lecture : BaseEntity
    {
        public string Title { get; set; }
        public string Summary { get; set; }

        public string? SubTitle { get; set; }
        public int OrderInCourse { get; set; }

        public long? CourseId { get; set; }
        public Course Course { get; set; }

        public List<Progress>? Progreses { get; set; } = new();

        public Test? Test { get; set; }
        public Image? Image { get; set; }
        public Video? Video { get; set; }
        public AdditionalFile? AdditionalFile { get; set; }
        public LectureMaterial? LectureMaterial { get; set; }
    }
}
