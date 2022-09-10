using Tartak.Magazyn.Models;
using Tartak.WebApp.Shared.Models;

namespace Tartak.Magazyn.Helpers
{
    public interface IProductSenderHelper
    {
        Task Send(ProductShopModel shopModel);
    }
}