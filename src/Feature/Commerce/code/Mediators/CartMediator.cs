using SitecoreCoffee.Feature.Commerce.Services;
using SitecoreCoffee.Feature.Commerce.ViewModel;

namespace SitecoreCoffee.Feature.Commerce.Mediators
{
    public class CartMediator : ICartMediator
    {
        private readonly ICartService _cartService;

        public CartMediator(ICartService cartService)
        {
            _cartService = cartService;
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

        public MediatorResponse<CartViewModel> IdenfifyContactInCart(string email)
        {
            var cart = _cartService.IdenfifyContactInCart(email);

            var viewModel = new CartViewModel()
            {
                Cart = cart
            };

            return GetMediatorResponse<CartViewModel>(MediatorCodes.CartMediator.IdenfifyContactInCart.Ok, viewModel);
        }

        public MediatorResponse<T> GetMediatorResponse<T>(string code, T viewModel = default(T))
        {
            var response = new MediatorResponse<T>
            {
                Code = code,
                ViewModel = viewModel
            };

            return response;
        }
    }
}