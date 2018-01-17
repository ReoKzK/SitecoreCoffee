using Sitecore.Commerce.Contacts;
using Sitecore.Commerce.Entities.Carts;
using Sitecore.Commerce.Services.Carts;
using Sitecore.Diagnostics;
using System.Linq;

namespace SitecoreCoffee.Feature.Commerce.Repositories
{
    public class CommerceCartRepository
    {
        /// <summary>
        /// Cart service provider
        /// </summary>
        private readonly CartServiceProvider _cartServiceProvider;

        /// <summary>
        /// Contact factory.
        /// </summary>
        private readonly ContactFactory _contactFactory;
        
        /// <summary>
        /// The shop name.
        /// </summary>
        private string _shopName;

        /// <summary>
        /// Gets or sets the name of the shop.
        /// </summary>
        /// <value>The name of the shop.</value>
        public string ShopName
        {
            get { return this._shopName; }
            set { this._shopName = value; }
        }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public string UserId
        {
            get
            {
                return this._contactFactory.GetContact();
            }
        }

        /// <summary>
        /// Creates new commerce cart repository
        /// </summary>
        /// <param name="cartServiceProvider"></param>
        /// <param name="contactFactory"></param>
        public CommerceCartRepository(
            CartServiceProvider cartServiceProvider,
            ContactFactory contactFactory)
        {
            _cartServiceProvider = cartServiceProvider;
            _contactFactory = contactFactory;
        }

        /// <summary>
        /// Gets the shopping cart for current contact
        /// </summary>
        /// <returns>The visitor's cart.</returns>
        public Cart GetCart()
        {
            return this.GetCart(this.UserId);
        }

        /// <summary>
        /// Adds product to the visitor's cart.
        /// </summary>
        /// <param name="productId">
        /// The product id.
        /// </param>
        /// <param name="quantity">
        /// The quantity.
        /// </param>
        /// <returns>
        /// The <see cref="Cart"/>.
        /// </returns>
        public Cart AddToCart(string productId, uint quantity)
        {
            Assert.ArgumentNotNull(productId, "productId");

            var cart = this.GetCart();

            if (string.IsNullOrWhiteSpace(cart.ShopName))
            {
                cart.ShopName = this.ShopName;
            }

            CartResult cartResult;

            var cartLineToChange = cart.Lines.FirstOrDefault(cl => cl.Product != null && cl.Product.ProductId == productId);

            if (cartLineToChange != null)
            {
                cartLineToChange.Quantity += quantity;
                var updateRequest = new UpdateCartLinesRequest(cart, new[] { cartLineToChange });
                cartResult = this._cartServiceProvider.UpdateCartLines(updateRequest);
            }

            else
            {
                var cartLine = new CartLine
                {
                    Product = new CartProduct
                    {
                        ProductId = productId
                    },
                    Quantity = quantity
                };

                //this.UpdateStockInformation(cartLine);
                var request = new AddCartLinesRequest(cart, new[] { cartLine });
                cartResult = this._cartServiceProvider.AddCartLines(request);
            }

            return cartResult.Cart;
        }

        /// <summary>
        /// Gets the shopping cart for specified contact
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <returns>
        /// The <see cref="GetCart" />.
        /// </returns>
        private Cart GetCart(string userId)
        {
            var request = new CreateOrResumeCartRequest(this.ShopName, userId);
            var result = this._cartServiceProvider.CreateOrResumeCart(request);

            return result.Cart;
        }

        public Cart LockCart()
        {
            Cart cart = GetCart();
            var result = _cartServiceProvider.LockCart(new LockCartRequest(cart));

            return result.Cart;
        }

        public bool DeleteCart()
        {
            Cart cart = GetCart();
            var result = _cartServiceProvider.DeleteCart(new DeleteCartRequest(cart));
            //_logService.Info($"CommerceCartRepository: Delete cart. Success: {result.Success}");

            return result.Success;
        }

        private Cart EnsureCorrectCartUserId(Cart cart)
        {
            if (cart.UserId != this.UserId)
            {
                Cart changes = new Cart { UserId = this.UserId };
                var updateCartRequest = new UpdateCartRequest(cart, changes);

                var result = this._cartServiceProvider.UpdateCart(updateCartRequest);
                if (result != null && result.Success)
                {
                    return result.Cart;
                }
            }

            return cart;
        }
    }
}