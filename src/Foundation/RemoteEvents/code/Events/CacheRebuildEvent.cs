namespace SitecoreCoffee.Foundation.RemoteEvents.Events
{
    /// <summary>
    /// Represents cache rebuild event
    /// </summary>
    public class CacheRebuildEvent
    {
        /// <summary>
        /// Flag for full rebuild
        /// </summary>
        public bool FullRebuild { get; set; }
    }
}
