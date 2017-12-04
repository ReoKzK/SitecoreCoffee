using Sitecore.Events;
using SitecoreCoffee.Foundation.RemoteEvents.Events;
using System;

namespace SitecoreCoffee.Foundation.RemoteEvents
{
    public class CacheRebuildEventArgs : EventArgs, IPassNativeEventArgs
    {
        public CacheRebuildEvent EventInfo { get; set; }

        public CacheRebuildEventArgs(CacheRebuildEvent @event)
        {
            EventInfo = @event;
        }
    }
}
