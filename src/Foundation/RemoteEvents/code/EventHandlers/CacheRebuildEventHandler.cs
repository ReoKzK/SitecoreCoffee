using Sitecore.Diagnostics;
using Sitecore.Publishing.Pipelines.PublishItem;
using System;
using System.Diagnostics;
using SitecoreCoffee.Foundation.RemoteEvents.CustomEventArgs;
using SitecoreCoffee.Foundation.RemoteEvents.EventRaisers;
using SitecoreCoffee.Foundation.RemoteEvents.Events;
using SitecoreCoffee.Foundation.RemoteEvents.Services;

namespace SitecoreCoffee.Foundation.RemoteEvents.EventHandlers
{
    public class CacheRebuildEventHandler
    {
        /// <summary>
        /// Static flag that indicates if any specific item was published
        /// </summary>
        protected static bool SpecificItemWasPublished = false;
        
        /// <summary>
        /// Method fired up while item processing finished (while publishing)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnItemProcessed(object sender, EventArgs args)
        {
            ItemProcessedEventArgs itemProcessedEventArgs = args as ItemProcessedEventArgs;
            PublishItemContext context = itemProcessedEventArgs?.Context;

            if (context?.VersionToPublish != null)
            {
                if (context.VersionToPublish.TemplateID.ToString() == Constants.SpecificItemTemplateId)
                {
                    SpecificItemWasPublished = true;
                }
            }
        }

        /// <summary>
        /// Method fired up on publish end - rebuilds cache if any specific item was published
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnPublishEnd(object sender, EventArgs args)
        {
            // 'publish:end' event provides one argument : instance of
            // Sitecore.Publishing.Publisher class performing the actual publish.

            //Publisher publisher = Event.ExtractParameter(args, 0) as Publisher;

            // Make sure that we were able to extract the publisher from event
            // An exception is thrown otherways.
            //Error.AssertObject(publisher, "Publisher");

            //var eventArgs = (Sitecore.Events.SitecoreEventArgs)args;

            if (SpecificItemWasPublished)
            {
                // - Raise rebuild cache action -
                var raiser = new CacheRebuildEventRaiser();
                raiser.RaiseEvent(
                    new CacheRebuildEvent()
                    {
                        FullRebuild = true
                    });

                // - Reset specific item published flag -
                SpecificItemWasPublished = false;
            }
        }

        public void OnCustomCacheRebuild(object sender, EventArgs args)
        {
            Assert.IsNotNull(args, "Args");

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                var cacheRebuildArgs = args as CacheRebuildEventArgs;

                Log.Info("CacheRebuildEventHandler: Rebuilding the cache.", this);

                var eventInfo = cacheRebuildArgs.EventInfo;

                var rebuildService = new CacheRebuildService();
                rebuildService.Rebuild(eventInfo.FullRebuild);

                stopWatch.Stop();
                Log.Info($"CacheRebuildEventHandler: Cache rebuilt in {stopWatch.ElapsedMilliseconds / 1000} seconds.", this);
            }

            catch (Exception exc)
            {
                Log.Warn($"CacheRebuildEventHandler: Exception while trying to rebuild cache {exc.Message}", this);
            }

            finally
            {
                stopWatch.Stop();
            }
        }
    }
}
