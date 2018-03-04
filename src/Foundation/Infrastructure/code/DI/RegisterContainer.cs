using System;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using SitecoreCoffee.Foundation.Infrastructure.Services;

namespace SitecoreCoffee.Foundation.Infrastructure.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddOfTypeInAssemblies(typeof(IController), SitecoreCoffeeAssembliesCondition);
            serviceCollection.AutoConfigureFromAssemblies(assembly => SitecoreCoffeeAssembliesCondition(assembly) && ExceptThisCondition(assembly));

            serviceCollection.AddTransient<IContextSwitchingService, ContextSwitchingService>();
        }

        private bool SitecoreCoffeeAssembliesCondition(Assembly assembly)
        {
            return assembly.FullName.StartsWith("SitecoreCoffee", StringComparison.InvariantCultureIgnoreCase);
        }

        private bool ExceptThisCondition(Assembly assembly)
        {
            return assembly != Assembly.GetExecutingAssembly();
        }
    }
}