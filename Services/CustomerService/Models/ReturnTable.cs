namespace CustomerService.Models
{
    public class ReturnTable
    {
        public int ReturnId { get; set; } // Primary Key
        public int CustomerId { get; set; } // Foreign Key
        public int ProductId { get; set; } // From Inventory Service
        public DateTime ReturnDate { get; set; }
        public string? Reason { get; set; }

        public CustomerInfo? Customer { get; set; }
    }
}
