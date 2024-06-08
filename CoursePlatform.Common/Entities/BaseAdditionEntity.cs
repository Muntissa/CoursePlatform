namespace CoursePlatform.Common.Entities
{
    public abstract class BaseAdditionEntity : BaseEntity
    {
        public long? LectureId { get; set; }
        public Lecture Lecture { get; set; }
        
    }
}
