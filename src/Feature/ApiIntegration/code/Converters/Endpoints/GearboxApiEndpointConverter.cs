using System;
using Sitecore.DataExchange.Converters.Endpoints;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using SitecoreCoffee.Feature.ApiIntegration.Models.ItemModels.Endpoints;
using SitecoreCoffee.Feature.ApiIntegration.Plugins;

namespace SitecoreCoffee.Feature.ApiIntegration.Converters.Endpoints
{
    public class GearboxApiEndpointConverter : BaseEndpointConverter
    {
        public GearboxApiEndpointConverter(IItemModelRepository repository) : base(repository)
        {
            this.SupportedTemplateIds.Add(Guid.Parse(Constants.EndpointTemplateId));
        }
        
        protected override void AddPlugins(ItemModel source, Endpoint endpoint)
        {
            var settings = new GearboxApiSettings();

            settings.ApiUrl = base.GetStringValue(source, GearboxApiEndpointItemModel.ApiUrl);

            endpoint.Plugins.Add(settings);
        }
    }
}