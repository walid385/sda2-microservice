using CustomerService.Data;
using CustomerService.Models;
using Microsoft.EntityFrameworkCore;


namespace CustomerService.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CustomerContext _context;

        public CartRepository(CustomerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartInProgress>> GetCartsByCustomerIdAsync(int customerId)
        {
            return await _context.CartsInProgress
                .Include(c => c.ItemLists)
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<CartInProgress> GetCartByIdAsync(int cartId)
        {
            return await _context.CartsInProgress
                .Include(c => c.ItemLists)
                .FirstOrDefaultAsync(c => c.CartId == cartId);
        }

        public async Task AddCartAsync(CartInProgress cart)
        {
            await _context.CartsInProgress.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartAsync(CartInProgress cart)
        {
            _context.CartsInProgress.Update(cart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartAsync(int cartId)
        {
            var cart = await GetCartByIdAsync(cartId);
            if (cart != null)
            {
                _context.CartsInProgress.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }
    }
}
