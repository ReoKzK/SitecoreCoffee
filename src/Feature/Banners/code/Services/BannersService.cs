using SitecoreCoffee.Foundation.ORM.Repositories;
using SitecoreCoffee.Feature.Banners.Models;

namespace SitecoreCoffee.Feature.Banners.Services
{
    public class BannersService
    {
        private readonly IDatasourceRepository _datasourceRepository;

        public BannersService(IDatasourceRepository datasourceRepository)
        {
            _datasourceRepository = datasourceRepository;
        }

        public IHeroBanner GetHeroBanner()
        {
            return _datasourceRepository.GetCurrentDatasourceItem<IHeroBanner>();
        }
    }
}