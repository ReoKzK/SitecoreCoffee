using System;
using Sitecore.Pipelines;
using SitecoreCoffee.Foundation.RemoteEvents.Events;

namespace SitecoreCoffee.Foundation.RemoteEvents.Pipelines
{
    public class Initialize
    {
        /// <summary>
        /// Initializes event subscription
        /// </summary>
        /// <param name="args">Args</param>
        public virtual void InitializeFromPipeline(PipelineArgs args)
        {
            var action = new Action<CacheRebuildEvent>(RaiseRemoteEvent);
            Sitecore.Eventing.EventManager.Subscribe<CacheRebuildEvent>(action);
        }

        /// <summary>
        /// Raises remote event
        /// </summary>
        /// <param name="cacheRebuildEvent"></param>
        private void RaiseRemoteEvent(CacheRebuildEvent cacheRebuildEvent)
        {
            Sitecore.Events.Event.RaiseEvent(Constants.CustomCacheRebuildEventNameRemote);
        }
    }
}