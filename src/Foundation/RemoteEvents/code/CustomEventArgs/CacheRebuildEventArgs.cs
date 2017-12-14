using System;
using Sitecore.Events;
using SitecoreCoffee.Foundation.RemoteEvents.Events;

namespace SitecoreCoffee.Foundation.RemoteEvents.CustomEventArgs
{
    [Serializable]
    public class CacheRebuildEventArgs : EventArgs, IPassNativeEventArgs
    {
        public CacheRebuildEvent EventInfo { get; set; }

        public CacheRebuildEventArgs(CacheRebuildEvent @event)
        {
            EventInfo = @event;
        }
    }
}
