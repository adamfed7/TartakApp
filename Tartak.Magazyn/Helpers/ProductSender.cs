using MassTransit;
using Tartak.Magazyn.Models;
using Tartak.WebApp.Shared.Models;

namespace Tartak.Magazyn.Helpers
{
    public class ProductSender : IProductSender
    {
        private readonly IBus _bus;
        private readonly ILogger<ProductSender> _logger;

        public ProductSender(IBus bus, ILogger<ProductSender> logger)
        {
            _bus = bus;
            _logger = logger;
        }
        public async Task Send(ProductShopModel message)
        {
            _logger.LogError("Quantity: " + message.QuantityInShop);
            await _bus.Publish(message);
        }
    }
}
