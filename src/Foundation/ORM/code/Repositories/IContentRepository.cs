using Sitecore.Globalization;

namespace SitecoreCoffee.Foundation.ORM.Repositories
{
    public interface IContentRepository
    {
        T GetCurrentItem<T>(bool isLazy = true, bool inferType = false) where T : class;
        T GetItem<T>(string path, bool isLazy = true, bool inferType = false) where T : class;
        T GetItem<T>(string path, Language language, bool isLazy = true, bool inferType = false) where T : class;
        T GetRelativeItem<T>(string query, bool isLazy = true, bool inferType = false) where T : class;
    }
}