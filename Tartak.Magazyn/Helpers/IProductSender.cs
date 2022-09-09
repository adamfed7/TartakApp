using Tartak.Magazyn.Models;

namespace Tartak.Magazyn.Helpers
{
    public interface IProductSender
    {
        Task Send(ProductShopModel shopModel);
    }
}