

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SGM.IOC.Dependencies
{
    public static class DependencyGeneric
    {
        public static IServiceCollection AddServicesOrRepositoryDependency(
            this IServiceCollection services, Type type,
            Assembly assembly,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var repositoryImplementations = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                             i.GetGenericTypeDefinition() == type))
                .ToList();

            foreach (var implementation in repositoryImplementations)
            {
                var concreteInterface = implementation.GetInterfaces()
                    .FirstOrDefault(i => !i.IsGenericType &&
                                       i.GetInterfaces()
                                        .Any(gi => gi.IsGenericType &&
                                                  gi.GetGenericTypeDefinition() == type));

                if (concreteInterface != null)
                {
                    services.Add(new ServiceDescriptor(
                        concreteInterface,
                        implementation,
                        lifetime));
                }
            }

            return services;
        }
       
    }
}
