using SitecoreCoffee.Foundation.ProductsSearch.Models;
using SitecoreCoffee.Foundation.ProductsSearch.Services;
using System;
using System.Collections.Generic;

namespace SitecoreCoffee.Foundation.ProductsSearch.Repositories
{
    public class ProductsRepository
    {
        private IProductsService _productsService;

        public ProductsRepository()
        {
            _productsService = new ProductsService();
        }

        public ProductsRepository(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productsService.GetProducts();
        }

        public Product GetProduct(String id)
        {
            return _productsService.GetProductById(id);
        }
    }
}