using System;

namespace SitecoreCoffee.Feature.ApiIntegration.Plugins
{
    public class GearboxApiSettings : Sitecore.DataExchange.IPlugin
    {
        public String ApiUrl { get; set; }

        public GearboxApiSettings()
        {
            
        }
    }
}