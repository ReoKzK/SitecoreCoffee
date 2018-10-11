using System;

namespace SitecoreCoffee.Foundation.Diagnostics.Profilers
{
    public class SitecoreProfiler : IDisposable
    {
        protected readonly string _name;

        public SitecoreProfiler(string name)
        {
            _name = name;
            Sitecore.Diagnostics.Profiler.StartOperation(_name);
        }

        public void Dispose()
        {
            Sitecore.Diagnostics.Profiler.EndOperation(_name);
        }
    }
}