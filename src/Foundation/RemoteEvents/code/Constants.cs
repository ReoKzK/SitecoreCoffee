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
        public const String SpecificItemTemplateId = "{C8D96B72-A780-49FE-8769-34F1AC8DB1B5}";
    }
}