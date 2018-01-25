﻿using SitecoreCoffee.Feature.Commerce.ViewModel;

namespace SitecoreCoffee.Feature.Commerce.Mediators
{
    public interface ICartMediator
    {
        MediatorResponse<CartViewModel> GetCart();

        MediatorResponse<T> GetMediatorResponse<T>(string code, T viewModel = default(T));

        MediatorResponse<CartViewModel> AddToCart(string productId, uint quantity);

        MediatorResponse<CartViewModel> IdenfifyContactInCart(string email, bool replaceExistingUserCart = false);
    }
}