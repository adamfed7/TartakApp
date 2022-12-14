using Microsoft.EntityFrameworkCore;
using Tartak.Magazyn.Models;
using Tartak.WebApp.Shared.Models;

namespace Tartak.Sklep.Context
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }
        public DbSet<ProductShopModel> Products { get; set; }
    }
}
