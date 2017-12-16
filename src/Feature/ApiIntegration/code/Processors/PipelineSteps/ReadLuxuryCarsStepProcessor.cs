using System;
using Refit;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Processors.PipelineSteps;
using SitecoreCoffee.Feature.ApiIntegration.Extensions;
using SitecoreCoffee.Feature.ApiIntegration.Models.WebApi;
using SitecoreCoffee.Feature.ApiIntegration.Plugins;

namespace SitecoreCoffee.Feature.ApiIntegration.Processors.PipelineSteps
{
    [RequiredEndpointPlugins(typeof(GearboxApiSettings))]
    public class ReadLuxuryCarsStepProcessor : BaseReadDataStepProcessor
    {
        public ReadLuxuryCarsStepProcessor()
        {
        }

        protected override void ReadData(Endpoint endpoint, PipelineStep pipelineStep, PipelineContext pipelineContext)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            if (pipelineStep == null)
            {
                throw new ArgumentNullException(nameof(pipelineStep));
            }

            if (pipelineContext == null)
            {
                throw new ArgumentNullException(nameof(pipelineContext));
            }

            var logger = pipelineContext.PipelineBatchContext.Logger;
            //
            //get the file path from the plugin on the endpoint
            var settings = endpoint.GetGearboxApiSettings();

            if (settings == null)
            {
                return;
            }

            if (String.IsNullOrWhiteSpace(settings.ApiUrl))
            {
                logger.Error(
                    "No Api Url is specified on the endpoint. " +
                    "(pipeline step: {0}, endpoint: {1})",
                    pipelineStep.Name, endpoint.Name);
                return;
            }
            
            var gearboxApi = RestService.For<IGearboxApi>(settings.ApiUrl);

            var luxuryCars = gearboxApi.GetLuxuryCars().Result;
            
            //add the data that was read from the file to a plugin
            var dataSettings = new IterableDataSettings(luxuryCars);

            logger.Info(
                "{0} items were read from the API. (pipeline step: {1}, endpoint: {2})",
                luxuryCars.Count, pipelineStep.Name, endpoint.Name);
            //
            //add the plugin to the pipeline context
            pipelineContext.Plugins.Add(dataSettings);
        }
    }
}