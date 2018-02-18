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

        public T GetItem<T>(string path, bool isLazy = true, bool inferType = false)
            where T : class
        {
            return _sitecoreContext.GetItem<T>(path, Language.Current, isLazy, inferType);
        }

        public T GetItem<T>(string path, Language language, bool isLazy = true, bool inferType = false)
            where T : class
        {
            return _sitecoreContext.GetItem<T>(path, language, isLazy, inferType);
        }

        public T GetCurrentItem<T>(bool isLazy = true, bool inferType = false)
            where T : class
        {
            return _sitecoreContext.GetCurrentItem<T>(isLazy, inferType);
        }
    }
}