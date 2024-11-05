using InventoryService.Data;
using InventoryService.Models;
using InventoryService.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryService.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public InventoryRepository(InventoryContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IEnumerable<ProductInventory>> GetAllProductsAsync()
        {
            return await _context.ProductInventories.ToListAsync();
        }

        public async Task<ProductInventory> GetProductByIdAsync(int productId)
        {
            return await _context.ProductInventories.FindAsync(productId);
        }

        public async Task AddProductAsync(ProductInventory product)
        {
            await _context.ProductInventories.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductInventory product)
        {
            _context.ProductInventories.Update(product);
            await _context.SaveChangesAsync();
            await CheckStockAndNotify(product.ProductId, product.InStock);
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await GetProductByIdAsync(productId);
            if (product != null)
            {
                _context.ProductInventories.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CheckStockAndNotify(int productId, int quantity)
        {
            if (quantity < 3) // Define a threshold
            {
                // Directly publish the event using IPublishEndpoint
                await _publishEndpoint.Publish<ILowStockEvent>(new
                {
                    ProductId = productId,
                    Quantity = quantity
                }, context =>
                {
                    context.SetRoutingKey("low_stock"); // Ensure the routing key matches
                });
            }
        }
    }
}
