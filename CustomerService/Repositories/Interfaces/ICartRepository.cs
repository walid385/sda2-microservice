using CustomerService.Models;

namespace CustomerService.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartInProgress>> GetCartsByCustomerIdAsync(int customerId);
        Task<CartInProgress> GetCartByIdAsync(int cartId);
        Task AddCartAsync(CartInProgress cart);
        Task UpdateCartAsync(CartInProgress cart);
        Task DeleteCartAsync(int cartId);
    }
}
