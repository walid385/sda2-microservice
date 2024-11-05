namespace CustomerService.Models
{
    public class CustomerInfo
    {
        public int CustomerId { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int Rewards { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }


        public ICollection<CartInProgress> CartsInProgress { get; set; }
        public ICollection<GiftCard> GiftCards { get; set; }
        public ICollection<ReturnTable> Returns { get; set; }
    }
}
