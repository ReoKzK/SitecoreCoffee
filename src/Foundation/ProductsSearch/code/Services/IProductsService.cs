using System.Collections.Generic;
using SitecoreCoffee.Foundation.ProductsSearch.Models;

namespace SitecoreCoffee.Foundation.ProductsSearch.Services
{
    public interface IProductsService
    {
        Product GetProductById(string Id);

        IEnumerable<Product> GetProducts();
    }
}