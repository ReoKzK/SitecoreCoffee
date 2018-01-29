using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Commerce.Services.Carts;
using Sitecore.Diagnostics;
using SitecoreCoffee.Feature.Commerce.Models;
using SitecoreCoffee.Feature.Commerce.Repositories;
using SitecoreCoffee.Feature.Commerce.Services.Commerce;
using SitecoreCoffee.Foundation.Logging;

namespace SitecoreCoffee.Feature.Commerce.Services
{
    public class CartService : ICartService
    {
        private readonly ICommerceCartRepository _commerceCartRepository;
        private readonly ICartManipulationsService _cartManipulationsService;
        private readonly IContactService _contactService;
        private readonly IMapperService _mapperService;
        private readonly ILoggerService _logService;
        
        public CartService(
            ICommerceCartRepository commerceCartRepository, 
            IContactService contactService,
            ICartManipulationsService cartManipulationsService,
            IMapperService mapperService,
            ILoggerService logService)
        {
            _commerceCartRepository = commerceCartRepository;
            _cartManipulationsService = cartManipulationsService;
            _contactService = contactService;
            _mapperService = mapperService;
            _logService = logService;
        }

        public Cart GetCart()
        {
            var cart = _commerceCartRepository.GetCart();
            return _mapperService.MapCommerceCart(cart);
        }

        public Cart AddToCart(string productId, uint quantity = 1)
        {
            var cart = _commerceCartRepository.AddToCart(productId, quantity);
            return _mapperService.MapCommerceCart(cart);
        }

        public Cart SetCartProperty(string key, object value)
        {
            _commerceCartRepository.SetCartProperty(key, value);

            var cart = _commerceCartRepository.GetCart();
            return _mapperService.MapCommerceCart(cart);
        }

        public bool DeleteCart()
        {
            return _commerceCartRepository.DeleteCart();
        }
        
        public List<Cart> GetCarts(CartSearchModel search)
        {
            var carts = _commerceCartRepository.GetCarts(search);
            return carts.Select(x => _mapperService.MapCommerceBaseCart(x)).ToList();
        }

        public Cart CreateNewCart(string cartName = "")
        {
            ////var cart = GetCart();

            //////if (cart.Products.Count > 0)
            ////{
            ////    DeleteCart();
            ////}

            _commerceCartRepository.DeleteCart();
            var cart = _commerceCartRepository.GetCart();

            _logService.Info($"CartService.CreateCart: Get new cart '{cart.ExternalId}'");

            return _mapperService.MapCommerceCart(cart);
        }
        
        public Cart IdenfifyContactInCart(string email, bool replaceExistingUserCart = false)
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

                var mergedCart = EnsureProperCart(cartFromAnonymous, replaceExistingUserCart);
                return mergedCart;
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
        /// <param name="replaceExistingUserCart"></param>
        /// <returns>
        /// The <see cref="Cart"/>.
        /// </returns>
        private Cart EnsureProperCart(Sitecore.Commerce.Entities.Carts.Cart cartFromAnonymous, bool replaceExistingUserCart = false)
        {
            Assert.ArgumentNotNull(cartFromAnonymous, "anonymousCart");

            var currentUserId = _commerceCartRepository.UserId;// _contactFactory.GetContact();
            var currentCart = _commerceCartRepository.GetCart(); //_commerceCartRepository.GetCart(userId);

            _logService.Info($"CartService.EnsureProperCart: Current Cart: UserId = '{currentCart.UserId}', ExternalID = '{currentCart.ExternalId}'");

            if (currentUserId != cartFromAnonymous.UserId)
            {
                _logService.Info($"CartService.EnsureProperCart: Current UserId ({currentUserId}) is different than the on in anonymous cart ({cartFromAnonymous.UserId})");
                _logService.Info($"CartService.EnsureProperCart: Current UserId ({currentUserId}): ExternalID: {currentCart.ExternalId}, Lines: {currentCart.Lines.Count}");
                _logService.Info($"CartService.EnsureProperCart: Anonymous UserId ({cartFromAnonymous.UserId}): ExternalID: {cartFromAnonymous.ExternalId}, Lines: {cartFromAnonymous.Lines.Count}");

                currentCart = _commerceCartRepository.EnsureCorrectCartUserId(currentCart);

                if (cartFromAnonymous.Lines.Any())
                {
                    if (replaceExistingUserCart)
                    {
                        _logService.Info("CartService.EnsureProperCart: Anonymous cart is not empty, replacing");
                        _cartManipulationsService.ReplaceCarts(cartFromAnonymous, currentCart);
                    }

                    else
                    {
                        _logService.Info("CartService.EnsureProperCart: Anonymous cart is not empty, merging");
                        _cartManipulationsService.MergeCarts(currentCart, cartFromAnonymous);
                    }
                }
            }

            currentCart = _commerceCartRepository.GetCart();

            return _mapperService.MapCommerceCart(currentCart);// this.UpdatePrices(currentCart);
        }
    }
}