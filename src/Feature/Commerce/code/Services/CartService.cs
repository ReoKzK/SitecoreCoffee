using System;
using System.Linq;
using Sitecore.Commerce.Services.Carts;
using Sitecore.Diagnostics;
using SitecoreCoffee.Feature.Commerce.Models;
using SitecoreCoffee.Feature.Commerce.Repositories;
using SitecoreCoffee.Foundation.Logging;

namespace SitecoreCoffee.Feature.Commerce.Services
{
    public class CartService : ICartService
    {
        private readonly ICommerceCartRepository _commerceCartRepository;
        private readonly IContactService _contactService;
        private readonly ILoggerService _logService;
        
        /// <summary>
        /// Cart service provider
        /// </summary>
        private readonly CartServiceProvider _cartServiceProvider;

        public CartService(
            ICommerceCartRepository commerceCartRepository, 
            IContactService contactService,
            ILoggerService logService,
            CartServiceProvider cartServiceProvider)
        {
            _commerceCartRepository = commerceCartRepository;
            _contactService = contactService;
            _logService = logService;

            _cartServiceProvider = cartServiceProvider;
        }

        public Cart GetCart()
        {
            var cart = _commerceCartRepository.GetCart();
            return MapCommerceCart(cart);
        }

        public Cart AddToCart(string productId, uint quantity = 1)
        {
            var cart = _commerceCartRepository.AddToCart(productId, quantity);
            return MapCommerceCart(cart);
        }

        public Cart IdenfifyContactInCart(string email)
        {
            try
            {
                var cartFromAnonymous = _commerceCartRepository.GetCart();

                //if (_xDbContactRepository.GetSession() != null &&
                //    !string.Equals(email, _xDbContactRepository.GetContact().Identifiers.Identifier, StringComparison.OrdinalIgnoreCase))

                if (_contactService.IdentifyAs(email))
                {
                    _logService.Info($"CartService.IdenfifyContactInCart: User identified as {email}");

                    //_xDbContactRepository.UpdatexDbIdentifier(email);
                    //_xDbContactRepository.UpdatexDbContactName(firstName, lastName);
                }

                var mergedCart = MergeCarts(cartFromAnonymous);
                return MapCommerceCart(mergedCart);
            }

            catch (Exception ex)
            {
                _logService.Error($"CartService.IdenfifyContactInCart: Failed {ex}");
                throw;
            }
        }

        /// <summary>
        /// Merges the carts.
        /// </summary>
        /// <param name="cartFromAnonymous">The anonymous visitor cart.</param>
        /// <returns>
        /// The <see cref="Cart"/>.
        /// </returns>
        public Sitecore.Commerce.Entities.Carts.Cart MergeCarts(Sitecore.Commerce.Entities.Carts.Cart cartFromAnonymous)
        {
            Assert.ArgumentNotNull(cartFromAnonymous, "anonymousCart");

            var currentUserId = _commerceCartRepository.UserId;// _contactFactory.GetContact();
            var currentCart = _commerceCartRepository.GetCart(); //_commerceCartRepository.GetCart(userId);

            _logService.Info($"CartService.MergeCarts: Current Cart: UserId = '{currentCart.UserId}', ExternalID = '{currentCart.ExternalId}'");

            if (currentUserId != cartFromAnonymous.UserId)
            {
                _logService.Info($"CartService.MergeCarts: Current UserId ({currentUserId}) is different than the on in anonymous cart ({cartFromAnonymous.UserId})");
                _logService.Info($"CartService.MergeCarts: Current UserId ({currentUserId}): ExternalID: {currentCart.ExternalId}, Lines: {currentCart.Lines.Count}");
                _logService.Info($"CartService.MergeCarts: Anonymous UserId ({cartFromAnonymous.UserId}): ExternalID: {cartFromAnonymous.ExternalId}, Lines: {cartFromAnonymous.Lines.Count}");

                currentCart = _commerceCartRepository.EnsureCorrectCartUserId(currentCart);

                if (cartFromAnonymous.Lines.Any())
                {
                    _logService.Info("CartService.MergeCarts: Anonymous cart is not empty, merging");

                    MergeCarts(currentCart, cartFromAnonymous);
                }
            }

            currentCart = _commerceCartRepository.GetCart();

            return currentCart;// this.UpdatePrices(currentCart);
        }

        /// <summary>
        /// Merges the carts.
        /// </summary>
        /// <param name="userCart">The user cart.</param>
        /// <param name="anonymousCart">The anonymous cart.</param>
        /// <returns>
        /// The merged cart.
        /// </returns>
        public Sitecore.Commerce.Entities.Carts.Cart MergeCarts(Sitecore.Commerce.Entities.Carts.Cart userCart, Sitecore.Commerce.Entities.Carts.Cart anonymousCart)
        {
            Assert.ArgumentNotNull(userCart, "userCart");
            Assert.ArgumentNotNull(anonymousCart, "anonymousCart");

            _logService.Info($"CartService.MergeCarts: Attempt to merge carts '{userCart.ExternalId}' and '{anonymousCart.ExternalId}'");

            userCart = _commerceCartRepository.EnsureCorrectCartUserId(userCart);

            if ((userCart.ShopName == anonymousCart.ShopName) && (userCart.ExternalId != anonymousCart.ExternalId))
            {
                var mergeCartRequest = new MergeCartRequest(anonymousCart, userCart);
                var result = _cartServiceProvider.MergeCart(mergeCartRequest);

                _logService.Info($"CartService.MergeCarts: Success: {result.Success}");

                var deleteResult = _cartServiceProvider.DeleteCart(new DeleteCartRequest(anonymousCart));

                _logService.Info($"CartService.MergeCarts: Delete anonymous cart '{anonymousCart.ExternalId}'. Success: {deleteResult.Success}");

                //return result.Cart;
            }

            return _commerceCartRepository.GetCart();// userCart;
        }

        public static Cart MapCommerceCart(Sitecore.Commerce.Entities.Carts.Cart cart)
        {
            return new Cart()
            {
                Name = cart.Name,
                TotalAmount = cart.Total?.Amount ?? decimal.Zero,
                Products = cart.Lines.Select(x => new Product()
                {
                    Name = x.Product?.ProductName,
                    Id = x.Product?.ProductId,
                    Sku = "",
                    Price = x.Product?.Price?.Amount ?? decimal.Zero
                }).ToList(),
                IsPopulated = cart.GetPropertyValue("IsPopulated")?.ToString() == "1",

                Info = new CartInternalInfo()
                {
                    ExternalId = cart.ExternalId,
                    ShopName = cart.ShopName,
                    UserId = cart.UserId
                }
            };
        }
    }
}