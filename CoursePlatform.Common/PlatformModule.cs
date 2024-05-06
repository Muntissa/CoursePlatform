using CoursePlatform.Common.Additional;
using CoursePlatform.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common
{
    public class PlatformModule : BaseModuleRegistration
    {
        public PlatformModule(IServiceCollection services)
        {
            this.Registrate(services);
        }
    }

    public static class AddPlatformExt
    {
        public static void AddPlatform(this IServiceCollection services)
        {
            services.AddTransient(typeof(IDataContext), typeof(CoursePlatformContext));
            services.AddSingleton<IModuleRegistration>(new PlatformModule(services));
            services.AddSingleton(typeof(IContextHelper), typeof(ContextHelper));
        }
    }
}
