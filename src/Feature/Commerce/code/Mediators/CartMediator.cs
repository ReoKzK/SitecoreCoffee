using SitecoreCoffee.Feature.Commerce.Models;
using SitecoreCoffee.Feature.Commerce.Services;
using SitecoreCoffee.Feature.Commerce.ViewModel;

namespace SitecoreCoffee.Feature.Commerce.Mediators
{
    public class CartMediator : BaseMediator, ICartMediator
    {
        private readonly ICartService _cartService;
        private readonly IContactService _contactService;

        public CartMediator(ICartService cartService, IContactService contactService)
        {
            _cartService = cartService;
            _contactService = contactService;
        }

        public MediatorResponse<CartViewModel> GetCart()
        {
            var cart = _cartService.GetCart();

            var viewModel = new CartViewModel()
            {
                Cart = cart
            };

            return GetMediatorResponse<CartViewModel>(MediatorCodes.CartMediator.GetCart.Ok, viewModel);
        }

        public MediatorResponse<CartViewModel> AddToCart(string productId, uint quantity)
        {
            var cart = _cartService.AddToCart(productId, quantity);

            var viewModel = new CartViewModel()
            {
                Cart = cart
            };

            return GetMediatorResponse<CartViewModel>(MediatorCodes.CartMediator.AddToCart.Ok, viewModel);
        }

        public MediatorResponse<CartViewModel> SetCartProperty(string key, object value)
        {
            var cart = _cartService.SetCartProperty(key, value);

            var viewModel = new CartViewModel()
            {
                Cart = cart
            };

            return GetMediatorResponse<CartViewModel>(MediatorCodes.CartMediator.SetCartProperty.Ok, viewModel);
        }

        public MediatorResponse<CartViewModel> IdenfifyContactInCart(string email, bool replaceExistingUserCart = false)
        {
            var cart = _cartService.IdenfifyContactInCart(email, replaceExistingUserCart);

            var viewModel = new CartViewModel()
            {
                Cart = cart
            };

            return GetMediatorResponse<CartViewModel>(MediatorCodes.CartMediator.IdenfifyContactInCart.Ok, viewModel);
        }

        public MediatorResponse<CartViewModel> CartDelete()
        {
            _cartService.DeleteCart();

            var cart = _cartService.GetCart();

            var viewModel = new CartViewModel()
            {
                Cart = cart
            };

            return GetMediatorResponse<CartViewModel>(MediatorCodes.CartMediator.CartDelete.Ok, viewModel);
        }

        public MediatorResponse<CartViewModel> CartCreate(string cartName = "")
        {
            var cart = _cartService.CreateNewCart(cartName);

            var viewModel = new CartViewModel()
            {
                Cart = cart
            };

            return GetMediatorResponse<CartViewModel>(MediatorCodes.CartMediator.CartCreate.Ok, viewModel);
        }

        public MediatorResponse<CartViewModel> SessionAbandon()
        {
            _contactService.SessionAbandon();

            var cart = _cartService.GetCart();

            var viewModel = new CartViewModel()
            {
                Cart = cart
            };

            return GetMediatorResponse<CartViewModel>(MediatorCodes.CartMediator.SessionAbandon.Ok, viewModel);
        }

        public MediatorResponse<CartAdminViewModel> SearchCarts(CartSearchModel search)
        {
            var carts = _cartService.GetCarts(search);

            var viewModel = new CartAdminViewModel()
            {
                Carts = carts
            };

            return GetMediatorResponse<CartAdminViewModel>(MediatorCodes.CartMediator.SearchCarts.Ok, viewModel);
        }
    }
}