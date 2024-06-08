using CoursePlatform.Common.Enums;

namespace CoursePlatform.Common.Entities
{
    public class AdditionalFile : BaseAdditionEntity
    {
        public string FilePath { get; set; }
        public FileType FileType { get; set; }
    }
}
