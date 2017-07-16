namespace SitecoreCoffee.Foundation.RemoteEvents
{
    public class CacheRebuildEventRaiser
    {
        public void RaiseEvent()
        {
            var parameters = new object[] { new CacheRebuildEventArgs() };

            Sitecore.Events.Event.RaiseEvent(CacheClearEventHandler.EventName);
            Sitecore.Eventing.EventManager.QueueEvent<CacheRebuildEventArgs>(new CacheRebuildEventArgs());
        }
    }
}
