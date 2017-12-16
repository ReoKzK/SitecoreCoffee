using System;
using Sitecore.DataExchange.Converters.PipelineSteps;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using SitecoreCoffee.Feature.ApiIntegration.Models.ItemModels.PipelineSteps;

namespace SitecoreCoffee.Feature.ApiIntegration.Converters.PipelineSteps
{
    public class ReadLuxuryCarsStepConverter : BasePipelineStepConverter
    {
        private static readonly Guid TemplateId = Guid.Parse(Constants.ReadLuxuryCarsPipelineStepTemplateId);

        public ReadLuxuryCarsStepConverter(IItemModelRepository repository) : base(repository)
        {
            this.SupportedTemplateIds.Add(TemplateId);
        }

        protected override void AddPlugins(ItemModel source, PipelineStep pipelineStep)
        {
            AddEndpointSettings(source, pipelineStep);
        }

        private void AddEndpointSettings(ItemModel source, PipelineStep pipelineStep)
        {
            var settings = new EndpointSettings();

            var endpointFrom = base.ConvertReferenceToModel<Endpoint>(source, ReadLuxuryCarsStepItemModel.EndpointFrom);

            if (endpointFrom != null)
            {
                settings.EndpointFrom = endpointFrom;
            }

            pipelineStep.Plugins.Add(settings);
        }
    }
}