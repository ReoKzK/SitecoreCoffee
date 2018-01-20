using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace SitecoreCoffee.Foundation.Infrastructure.DI
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddOfTypeInAssemblies(this IServiceCollection serviceCollection,
            Type dependencyInterface, Func<Assembly, bool> condition)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(condition))
            {
                var types = assembly.GetTypes().Where(x => !x.IsInterface && dependencyInterface.IsAssignableFrom(x));
                foreach (var type in types)
                {
                    var serviceDescriptor = new ServiceDescriptor(type, type, ServiceLifetime.Transient);

                    if (!serviceCollection.Contains(serviceDescriptor))
                    {
                        serviceCollection.Add(serviceDescriptor);
                    }
                }
            }
        }

        public static void AutoConfigureFromAssemblies(this IServiceCollection serviceCollection, Func<Assembly, bool> condition)
        {
            var configuratorType = typeof(IServicesConfigurator);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(condition))
            {
                var configuratorTypes = assembly.GetTypes().Where(x => !x.IsInterface && configuratorType.IsAssignableFrom(x));
                foreach (var type in configuratorTypes)
                {
                    (Activator.CreateInstance(type) as IServicesConfigurator)?.Configure(serviceCollection);
                }
            }
        }
    }
}