using CoursePlatform.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Additional
{
    public static class ContextExtansions
    {
        public static async Task InContext<T>(this IServiceProvider services, Func<T, Task> func) where T : IDataContext
        {
            await services.GetRequiredService<IContextHelper>().InContext<T>(func);
        }
    }
}
