using System;
using Glass.Mapper.Sc;
using Sitecore.Globalization;

namespace SitecoreCoffee.Foundation.ORM.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly ISitecoreContext _sitecoreContext;

        public ContentRepository(ISitecoreContext sitecoreContext)
        {
            _sitecoreContext = sitecoreContext;
        }

        /// <summary>
        /// Gets specified item in current language
        /// </summary>
        /// <typeparam name="T">Type to cast</typeparam>
        /// <param name="path">Item path</param>
        /// <param name="isLazy">Lazy load</param>
        /// <param name="inferType">Infer type</param>
        /// <returns>Item casted to specified type</returns>
        public T GetItem<T>(string path, bool isLazy = true, bool inferType = false)
            where T : class
        {
            return _sitecoreContext.GetItem<T>(path, Language.Current, isLazy, inferType);
        }

        /// <summary>
        /// Gets specified item in specified language
        /// </summary>
        /// <typeparam name="T">Type to cast</typeparam>
        /// <param name="path">Item path</param>
        /// <param name="language">Language</param>
        /// <param name="isLazy">Lazy load</param>
        /// <param name="inferType">Infer type</param>
        /// <returns>Item casted to specified type</returns>
        public T GetItem<T>(string path, Language language, bool isLazy = true, bool inferType = false)
            where T : class
        {
            return _sitecoreContext.GetItem<T>(path, language, isLazy, inferType);
        }

        public T GetItem<T>(Guid id, bool isLazy = true, bool inferType = false)
            where T : class
        {
            return _sitecoreContext.GetItem<T>(id, Language.Current, isLazy, inferType);
        }

        public T GetItem<T>(Guid id, Language language, bool isLazy = true, bool inferType = false)
            where T : class
        {
            return _sitecoreContext.GetItem<T>(id, language, isLazy, inferType);
        }

        /// <summary>
        /// Get current context item
        /// </summary>
        /// <typeparam name="T">Type to cast</typeparam>
        /// <param name="isLazy">Lazy load</param>
        /// <param name="inferType">Infer type</param>
        /// <returns>Item casted to specified type</returns>
        public T GetCurrentItem<T>(bool isLazy = true, bool inferType = false)
            where T : class
        {
            return _sitecoreContext.GetCurrentItem<T>(isLazy, inferType);
        }

        public T GetRelativeItem<T>(string query, bool isLazy = true, bool inferType = false)
            where T : class
        {
            return _sitecoreContext.QuerySingleRelative<T>(query, isLazy, inferType);
        }
    }
}