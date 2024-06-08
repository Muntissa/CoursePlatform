using CoursePlatform.Common.Interfaces;

namespace CoursePlatform.Common.Entities
{
    public abstract class BaseEntity : IDataEntity
    {
        public long Id { get; set; }
    }
}
