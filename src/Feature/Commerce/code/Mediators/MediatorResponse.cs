using System.Collections.Generic;

namespace SitecoreCoffee.Feature.Commerce.Mediators
{
    public class MediatorResponse
    {
        public string Code { get; set; }
    }


    public class MediatorResponse<T> : MediatorResponse
    {
        public T ViewModel { get; set; }
    }

}