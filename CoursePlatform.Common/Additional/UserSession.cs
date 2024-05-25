using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Interfaces;

namespace CoursePlatform.Common.Additional
{
    public class UserSession : IUserSession
    {   
        private User? UserS { get; set; }
        private CoursePlatformContext _context;

        public UserSession(CoursePlatformContext context) 
        {
            _context = context;
        }

        public User? GetUserSession()
        {
            return _context.Set<User>().FirstOrDefault();
        }

        public void SetUserSession(User user)
        {
            UserS = user;
        }
    }
}

