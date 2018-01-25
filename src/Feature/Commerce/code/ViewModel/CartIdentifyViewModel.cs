using System.ComponentModel;

namespace SitecoreCoffee.Feature.Commerce.ViewModel
{
    public class CartIdentifyViewModel
    {
        public string Email { get; set; }

        [DisplayName("Replace existing user cart")]
        public bool ReplaceExistingUserCart { get; set; }
    }
}