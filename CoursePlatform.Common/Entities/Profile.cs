namespace CoursePlatform.Common.Entities
{
    public class Profile : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }

        public long? UserId { get; set; }
        public User User { get; set; }
    }
}
