using Sitecore.Commerce.Entities.Carts;
using Sitecore.Commerce.Services.Carts;
using Sitecore.Diagnostics;
using SitecoreCoffee.Feature.Commerce.Pipelines.Carts.ReplaceCart;
using SitecoreCoffee.Feature.Commerce.Repositories;
using SitecoreCoffee.Foundation.Logging;

namespace SitecoreCoffee.Feature.Commerce.Services.Commerce
{
    public class CartManipulationsService : ICartManipulationsService
    {
        private readonly ICommerceCartRepository _commerceCartRepository;
        private readonly ILoggerService _logService;
        
        private readonly CartServiceProvider _cartServiceProvider;

        public CartManipulationsService(
            ICommerceCartRepository commerceCartRepository,
            ILoggerService logService,
            CartServiceProvider cartServiceProvider)
        {
            _commerceCartRepository = commerceCartRepository;
            _logService = logService;

            _cartServiceProvider = cartServiceProvider;
        }

        /// <summary>
        /// Merges the carts.
        /// </summary>
        /// <param name="userCart">The user cart.</param>
        /// <param name="anonymousCart">The anonymous cart.</param>
        /// <returns>
        /// The merged cart.
        /// </returns>
        public Cart MergeCarts(Cart userCart, Cart anonymousCart)
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

        /// <summary>
        /// Replaces target cart content with source one
        /// </summary>
        /// <param name="fromCart"></param>
        /// <param name="toCart"></param>
        /// <returns></returns>
        public Cart ReplaceCarts(Cart fromCart, Cart toCart)
        {
            Assert.ArgumentNotNull(fromCart, "fromCart");
            Assert.ArgumentNotNull(toCart, "toCart");

            _logService.Info($"CartManipulationsService.ReplaceCarts: Attempt to replace cart '{toCart.ExternalId}' with content from '{fromCart.ExternalId}'");

            fromCart = _commerceCartRepository.EnsureCorrectCartUserId(fromCart);

            if ((fromCart.ShopName == toCart.ShopName) && (fromCart.ExternalId != toCart.ExternalId))
            {
                var replaceCartRequest = new ReplaceCartRequest(fromCart, toCart);
                var result = _cartServiceProvider.ReplaceCart(replaceCartRequest);

                _logService.Info($"CartManipulationsService.ReplaceCarts: Success: {result.Success}");

                var deleteResult = _cartServiceProvider.DeleteCart(new DeleteCartRequest(fromCart));

                _logService.Info($"CartManipulationsService.ReplaceCarts: Delete source cart '{fromCart.ExternalId}'. Success: {deleteResult.Success}");

                //return result.Cart;
            }

            return _commerceCartRepository.GetCart();// userCart;
        }
    }
}