using InventoryService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryService.Repositories
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<ProductInventory>> GetAllProductsAsync();
        Task<ProductInventory> GetProductByIdAsync(int productId);
        Task AddProductAsync(ProductInventory product);
        Task UpdateProductAsync(ProductInventory product);
        Task DeleteProductAsync(int productId);
    }
}
