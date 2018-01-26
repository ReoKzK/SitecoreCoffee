using System.Collections.Generic;

namespace SitecoreCoffee.Feature.Commerce.Models
{
    public class Cart
    {
        public string Name { get; set; }

        public List<Product> Products { get; set; }

        public decimal TotalAmount { get; set; }

        public CartCustomProperties Properties { get; set; }
        
        public CartInternalInfo Info { get; set; }
    }
}