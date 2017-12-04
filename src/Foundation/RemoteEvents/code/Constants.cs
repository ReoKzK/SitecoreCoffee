using System;

namespace SitecoreCoffee.Foundation.RemoteEvents
{
    public static class Constants
    {
        /// <summary>
        /// Event name (local)
        /// </summary>
        public static readonly String CustomCacheRebuildEventName = "customCache:rebuild";

        /// <summary>
        /// Event name (remote)
        /// </summary>
        public static readonly String CustomCacheRebuildEventNameRemote = CustomCacheRebuildEventName + ":remote";

        /// <summary>
        /// Specific item template ID
        /// </summary>
        public const String SpecificItemTemplateId = "{54237CC1-6502-4873-9580-A2F6141D482F}";
    }
}