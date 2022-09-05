﻿using Tartak.Magazyn.Context;
using Tartak.Magazyn.Models;

namespace Tartak.Magazyn.Helpers
{
    public class ProductHelper : IProductHelper
    {
        private readonly WarehouseContext _context;

        public ProductHelper(WarehouseContext context)
        {
            _context = context;
        }
        public IEnumerable<ProductWarehouseModel> GetAllProducts()
        {
            return _context.Products.Where(x => x.IsActual == true);
        }
        public ProductWarehouseModel GetProductById(int id)
        {
            return _context.Products.Where(x => x.IsActual == true && x.Id == id).Single();
        }
        public async Task AddProductAsync(ProductWarehouseModel product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProductAsync(ProductWarehouseModel product)
        {
            var trackingProduct = GetProductById(product.Id);
            trackingProduct.Name = product.Name;
            trackingProduct.Description = product.Description;
            trackingProduct.PurchasePrice = product.PurchasePrice;
            trackingProduct.QuantityInWarehouse = product.QuantityInWarehouse;
            _context.Products.Update(trackingProduct);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(ProductWarehouseModel product)
        {
            _context.Products.Where(x => x.Id == product.Id).Single().IsActual = false;
            await _context.SaveChangesAsync();
        }
    }
}