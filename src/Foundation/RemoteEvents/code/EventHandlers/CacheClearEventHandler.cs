using Sitecore.Diagnostics;
using Sitecore.Events;
using Sitecore.Pipelines;
using Sitecore.Publishing;
using Sitecore.Publishing.Pipelines.PublishItem;
using SitecoreCoffee.Foundation.RemoteEvents.Events;
using System;
using System.Diagnostics;
using SitecoreCoffee.Foundation.RemoteEvents.EventRaisers;

namespace SitecoreCoffee.Foundation.RemoteEvents.EventHandlers
{
    public class CacheClearEventHandler
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
        public void ItemProcessed(object sender, EventArgs args)
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
        /// Method fired up on publish end - clears cache if any specific item was published
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
                // - Clear cache -
                var raiser = new CacheRebuildEventRaiser();
                raiser.RaiseEvent();

                // - Reset hotel published flag -
                SpecificItemWasPublished = false;
            }
        }

        public void OnCustomCacheRebuild(object sender, EventArgs args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            long timeElapsed = 0;

            try
            {
                var eventInfo = Event.ExtractParameter(args, 0) as CacheRebuildEvent;
                // check for full rebuild

                //if (publisher.Options.RootItem.TemplateID.ToString() == HotelTemplateId || (publisher.Options.Mode != PublishMode.SingleItem && publisher.Options.RootItem.Axes.GetDescendants().Any(x => x.TemplateID.ToString() == HotelTemplateId)))

                //|| (eventArgs.EventName == "publish:end:remote"
                //        && (publisher.Options.RootItem.TemplateID.ToString() == HotelTemplateId
                //            || (publisher.Options.Mode != PublishMode.SingleItem && publisher.Options.RootItem.Axes.GetDescendants().Any(x => x.TemplateID.ToString() == HotelTemplateId))))

                Log.Info("CacheClearEventHandler: Clearing the cache.", this);

                // - Here clear the cache -

                timeElapsed = stopWatch.ElapsedMilliseconds;
            }

            catch (Exception exc)
            {
                Log.Warn("CacheClearEventHandler: Exception while trying to clear cache " + exc.Message, this);
            }

            finally
            {
                stopWatch.Stop();
            }
        }
    }
}
