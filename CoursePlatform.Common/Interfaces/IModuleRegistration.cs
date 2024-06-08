using Microsoft.Extensions.DependencyInjection;

namespace CoursePlatform.Common.Interfaces
{
    public interface IModuleRegistration
    {
        void Registrate(IServiceCollection services);
    }
}
