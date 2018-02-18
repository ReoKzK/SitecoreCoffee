using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
using Sitecore.StringExtensions;

namespace SitecoreCoffee.Foundation.ORM.Repositories
{
    public class DatasourceRepository : IDatasourceRepository
    {
        private readonly IRenderingContext _renderingContext;

        private readonly IContentRepository _contentRepository;

        private readonly IGlassHtml _glassHtml;

        public DatasourceRepository(
            IRenderingContext renderingContext,
            IContentRepository contentRepository,
            IGlassHtml glassHtml)
        {
            _renderingContext = renderingContext;
            _contentRepository = contentRepository;
            _glassHtml = glassHtml;
        }

        public string DataSource => _renderingContext.GetDataSource();

        public T GetCurrentDatasourceItem<T>(bool isLazy = true, bool inferType = false)
            where T : class
        {
            return DataSource.IsNullOrEmpty()
                ? null
                : _contentRepository.GetItem<T>(DataSource, isLazy, inferType);
        }
    }
}