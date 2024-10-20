using CustomerService.Models;

namespace CustomerService.Repositories
{
    public interface IGiftCardRepository
    {
        Task<IEnumerable<GiftCard>> GetGiftCardsByCustomerIdAsync(int customerId);
        Task<GiftCard> GetGiftCardByIdAsync(int giftCardId);
        Task AddGiftCardAsync(GiftCard giftCard);
        Task UpdateGiftCardAsync(GiftCard giftCard);
        Task DeleteGiftCardAsync(int giftCardId);
    }
}
