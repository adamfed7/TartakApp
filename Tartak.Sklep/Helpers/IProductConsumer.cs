using MassTransit;
using Tartak.Magazyn.Models;
using Tartak.WebApp.Shared.Models;

namespace Tartak.Sklep.Helpers
{
    public interface IProductConsumer : IConsumer<ProductShopModel> { }
}