using MassTransit;
using Tartak.WebApp.Shared.Models;

namespace Tartak.Magazyn.Helpers
{
    public class ProductSenderHelper : IProductSenderHelper
    {
        private readonly IBus _bus;

        public ProductSenderHelper(IBus bus)
        {
            _bus = bus;
        }
        public async Task Send(ProductShopModel message)
        {
            await _bus.Publish(message);
        }
    }
}
