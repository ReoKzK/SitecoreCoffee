using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace SitecoreCoffee.Feature.ApiIntegration.Models.WebApi
{
    public interface IGearboxApi
    {
        [Get("/api/cars?category=luxury")]
        Task<List<Car>> GetLuxuryCars();

        [Get("/api/cars/{id}")]
        Task<Car> GetCar(string id);
    }
}
