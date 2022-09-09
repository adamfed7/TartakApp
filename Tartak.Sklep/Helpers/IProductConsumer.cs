using MassTransit;
using Tartak.Magazyn.Models;

namespace Tartak.Sklep.Helpers
{
    public interface IProductConsumer : IConsumer<ProductShopModel> { }
}