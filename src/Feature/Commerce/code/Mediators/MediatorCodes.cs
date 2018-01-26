namespace SitecoreCoffee.Feature.Commerce.Mediators
{
    public static class MediatorCodes
    {
        public static class CartMediator
        {
            public static class GetCart
            {
                public const string Ok = "CartMediator.GetCart.Ok";
                public const string Fail = "CartMediator.GetCart.Fail";
            }

            public static class AddToCart
            {
                public const string Ok = "CartMediator.AddToCart.Ok";
                public const string Fail = "CartMediator.AddToCart.Fail";
            }

            public static class IdenfifyContactInCart
            {
                public const string Ok = "CartMediator.IdenfifyContactInCart.Ok";
                public const string Fail = "CartMediator.IdenfifyContactInCart.Fail";
            }

            public static class CartDelete
            {
                public const string Ok = "CartMediator.CartDelete.Ok";
                public const string Fail = "CartMediator.CartDelete.Fail";
            }

            public static class SetCartProperty
            {
                public const string Ok = "CartMediator.SetCartProperty.Ok";
                public const string Fail = "CartMediator.SetCartProperty.Fail";
            }

            public static class SessionAbandon
            {
                public const string Ok = "CartMediator.SessionAbandon.Ok";
                public const string Fail = "CartMediator.SessionAbandon.Fail";
            }
        }
    }
}