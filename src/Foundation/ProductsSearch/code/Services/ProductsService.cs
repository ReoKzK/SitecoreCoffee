using SitecoreCoffee.Foundation.ProductsSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SitecoreCoffee.Foundation.ProductsSearch.Services
{
    public class ProductsService : IProductsService
    {
        private IEnumerable<Product> _productsMock = new List<Product>()
        {
            new Product()
            {
                Id = "PRD-001",
                Name = "Test product 001",
                Description = "Just a test product",
                Price = 12.50,
                PriceCurrency = "GBP"
            },

            new Product()
            {
                Id = "PRD-002",
                Name = "Test product 002",
                Description = "Just a test product",
                Price = 2.50,
                PriceCurrency = "GBP"
            },

            new Product()
            {
                Id = "PRD-003",
                Name = "Test product 003",
                Description = "Just a test product",
                Price = 5.00,
                PriceCurrency = "PLN"
            }
        };

        public IEnumerable<Product> GetProducts()
        {
            return _productsMock;
        }

        public Product GetProductById(String id)
        {
            return _productsMock.FirstOrDefault(x => x.Id == id);
        }
    }
}