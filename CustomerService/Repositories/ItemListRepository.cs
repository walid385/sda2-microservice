using CustomerService.Data;
using CustomerService.Models;
using Microsoft.EntityFrameworkCore;


namespace CustomerService.Repositories
{
    public class ItemListRepository : IItemListRepository
    {
        private readonly CustomerContext _context;

        public ItemListRepository(CustomerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemList>> GetItemsByCartIdAsync(int cartId)
        {
            return await _context.ItemLists
                .Where(i => i.CartId == cartId)
                .ToListAsync();
        }

        public async Task<ItemList> GetItemByIdAsync(int itemListId)
        {
            return await _context.ItemLists.FindAsync(itemListId);
        }

        public async Task AddItemAsync(ItemList item)
        {
            await _context.ItemLists.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(ItemList item)
        {
            _context.ItemLists.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int itemListId)
        {
            var item = await GetItemByIdAsync(itemListId);
            if (item != null)
            {
                _context.ItemLists.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
