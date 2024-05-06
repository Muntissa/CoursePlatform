using CoursePlatform.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CoursePlatform.Common.Additional
{
    public class BaseModuleRegistration : IModuleRegistration
    {
        public virtual void Registrate(IServiceCollection services)
        {
            var allTypes = GetType().Assembly.GetTypes().Where(s => !s.IsAbstract && !s.IsInterface && s != null);
            var interfaces = new Type[] { typeof(IDataEntity) };

            foreach (var i in interfaces)
                RegistrateInterfaceImpl(i, allTypes, services);
        }

        public static void RegistrateInterfaceImpl(Type interfacetype, IEnumerable<Type> types, IServiceCollection services)
        {
            var typesToReg = types.Where(s => s.GetInterfaces().FirstOrDefault(i => i == interfacetype) != null);

            foreach (Type type in typesToReg)
            {
                try
                {
                    if (typesToReg == typeof(IDataContext))
                    {
                        services.AddTransient(typeof(IDataContext), type);
                        continue;
                    }

                    services.AddSingleton(interfacetype, type);
                    
                }
                catch (Exception value)
                {
                    Console.WriteLine($"Ошибка иницаилазации {interfacetype} - {type}: {value}");
                }
            }
        }
    }
}
