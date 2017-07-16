using System;

namespace SitecoreCoffee.Foundation.ProductsSearch.Models
{
    public class Product
    {
        public String Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public double Price { get; set; }

        public String PriceCurrency { get; set; }
    }
}