using Microsoft.EntityFrameworkCore;
using Tartak.Magazyn.Models;

namespace Tartak.Magazyn.Context
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options) { }
        public DbSet<ProductWarehouseModel> Products { get; set; }
    }
}
