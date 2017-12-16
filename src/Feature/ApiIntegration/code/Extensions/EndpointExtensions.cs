using Sitecore.DataExchange.Models;
using SitecoreCoffee.Feature.ApiIntegration.Plugins;

namespace SitecoreCoffee.Feature.ApiIntegration.Extensions
{
    public static class EndpointExtensions
    {
        public static GearboxApiSettings GetGearboxApiSettings(this Endpoint endpoint)
        {
            return endpoint.GetPlugin<GearboxApiSettings>();
        }

        public static bool HasGearboxApiSettings(this Endpoint endpoint)
        {
            return (GetGearboxApiSettings(endpoint) != null);
        }
    }
}