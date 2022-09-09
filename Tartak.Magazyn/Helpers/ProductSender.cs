using MassTransit;
using Tartak.Magazyn.Models;
using Tartak.WebApp.Shared.Models;

namespace Tartak.Magazyn.Helpers
{
    public class ProductSender : IProductSender
    {
        private readonly IBus _bus;

        public ProductSender(IBus bus)
        {
            _bus = bus;
        }
        public async Task Send(ProductShopModel message)
        {
            await _bus.Publish(message);
        }
    }
}
