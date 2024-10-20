using CustomerService.Data;
using CustomerService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Repositories
{
    public class GiftCardRepository : IGiftCardRepository
    {
        private readonly CustomerContext _context;

        public GiftCardRepository(CustomerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GiftCard>> GetGiftCardsByCustomerIdAsync(int customerId)
        {
            return await _context.GiftCards
                .Where(g => g.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<GiftCard> GetGiftCardByIdAsync(int giftCardId)
        {
            return await _context.GiftCards.FindAsync(giftCardId);
        }

        public async Task AddGiftCardAsync(GiftCard giftCard)
        {
            await _context.GiftCards.AddAsync(giftCard);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGiftCardAsync(GiftCard giftCard)
        {
            _context.GiftCards.Update(giftCard);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGiftCardAsync(int giftCardId)
        {
            var giftCard = await GetGiftCardByIdAsync(giftCardId);
            if (giftCard != null)
            {
                _context.GiftCards.Remove(giftCard);
                await _context.SaveChangesAsync();
            }
        }
    }
}
