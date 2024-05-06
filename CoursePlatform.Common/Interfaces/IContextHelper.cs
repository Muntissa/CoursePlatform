using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Interfaces
{
    public interface IContextHelper
    {
        public Task InContext<T>(Func<T, Task> action) where T : IDataContext;
    }
}
