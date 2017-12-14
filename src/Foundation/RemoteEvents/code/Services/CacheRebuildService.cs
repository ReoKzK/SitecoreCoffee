using Sitecore.Diagnostics;

namespace SitecoreCoffee.Foundation.RemoteEvents.Services
{
    public class CacheRebuildService
    {
        public void Rebuild(bool FullRebuild)
        {
            Log.Info($"CacheRebuildService: Cache rebuilt (full: {FullRebuild})", this);
        }
    }
}