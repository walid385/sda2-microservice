namespace CustomerService.DTOs
{
    public class GiftCardDto
    {
        public int GiftCardId { get; set; } 
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
