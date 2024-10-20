namespace CustomerService.Models
{
    public class GiftCard
    {
        public int GiftCardId { get; set; } // Primary Key
        public int CustomerId { get; set; } // Foreign Key
        public decimal Balance { get; set; }
        public DateTime ExpiryDate { get; set; }

        public CustomerInfo Customer { get; set; }
    }
}
