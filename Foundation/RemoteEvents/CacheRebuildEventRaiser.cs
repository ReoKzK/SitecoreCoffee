using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreCoffee.RemoteEvents
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
