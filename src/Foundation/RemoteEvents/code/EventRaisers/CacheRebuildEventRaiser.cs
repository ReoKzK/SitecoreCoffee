using SitecoreCoffee.Foundation.RemoteEvents.CustomEventArgs;
using SitecoreCoffee.Foundation.RemoteEvents.Events;

namespace SitecoreCoffee.Foundation.RemoteEvents.EventRaisers
{
    public class CacheRebuildEventRaiser
    {
        public void RaiseEvent()
        {
            var @event = new CacheRebuildEvent();
            RaiseEvent(@event);
        }

        public void RaiseEvent(CacheRebuildEvent @event)
        {
            var eventArgs = new object[] { new CacheRebuildEventArgs(@event) };

            Sitecore.Events.Event.RaiseEvent(Constants.CustomCacheRebuildEventName, eventArgs);
            Sitecore.Eventing.EventManager.QueueEvent<CacheRebuildEvent>(@event);
        }
    }
}
