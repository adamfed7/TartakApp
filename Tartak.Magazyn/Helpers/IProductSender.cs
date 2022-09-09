using Tartak.Magazyn.Models;
using Tartak.WebApp.Shared.Models;

namespace Tartak.Magazyn.Helpers
{
    public interface IProductSender
    {
        Task Send(ProductShopModel shopModel);
    }
}