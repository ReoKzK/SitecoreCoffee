using System.Threading;
using SitecoreCoffee.Feature.Commerce.Models.CarPartsShop;

namespace SitecoreCoffee.Feature.Commerce.Services.CarPartsShop
{
    public class CarPartsShopSystemService : ICarPartsShopSystemService
    {
        private CancellationTokenSource _cts;

        public HeartbeatResponse Heartbeat()
        {
            return new HeartbeatResponse()
            {
                IsAlive = true,
                ErrorCode = "OK",
                ErrorMessage = "System healthy"
            };
        }
    }
}