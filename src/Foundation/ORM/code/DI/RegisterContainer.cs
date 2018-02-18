using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
using Glass.Mapper.Sc.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using SitecoreCoffee.Foundation.ORM.Repositories;

namespace SitecoreCoffee.Foundation.ORM.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGlassHtml, GlassHtml>();
            serviceCollection.AddTransient<IRenderingContext, RenderingContextMvcWrapper>();
            serviceCollection.AddTransient<ISitecoreContext, SitecoreContext>();

            serviceCollection.AddTransient<IContentRepository, ContentRepository>();
            serviceCollection.AddTransient<IDatasourceRepository, DatasourceRepository>();
        }
    }
}