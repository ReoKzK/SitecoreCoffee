namespace SitecoreCoffee.Feature.Commerce.Mediators
{
    public class BaseMediator
    {
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