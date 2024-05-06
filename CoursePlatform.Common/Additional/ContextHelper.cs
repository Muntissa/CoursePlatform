using CoursePlatform.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Additional
{
    public class ContextHelper : IContextHelper
    {
        private IServiceProvider _services;

        public ContextHelper(IServiceProvider serviceProvider)
        {
            _services = serviceProvider;
        }

        public async Task InContext<T>(Func<T, Task> action) where T : IDataContext
        {
            try
            {
                using var scope = _services.CreateScope();
                using var context = scope.ServiceProvider.GetRequiredService<T>();
                await action(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
