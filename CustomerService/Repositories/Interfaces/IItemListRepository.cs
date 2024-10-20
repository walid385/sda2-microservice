using CustomerService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerService.Repositories
{
    public interface IItemListRepository
    {
        Task<IEnumerable<ItemList>> GetItemsByCartIdAsync(int cartId);
        Task<ItemList> GetItemByIdAsync(int itemListId);
        Task AddItemAsync(ItemList item);
        Task UpdateItemAsync(ItemList item);
        Task DeleteItemAsync(int itemListId);
    }
}
