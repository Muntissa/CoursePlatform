using CoursePlatform.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Interfaces
{
    public interface IUserSession
    {
        public User GetUserSession();

        public void SetUserSession(User user);
    }
}
