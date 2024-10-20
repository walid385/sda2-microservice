using System;

namespace CustomerService.DTOs
{
    public class ReturnTableDto
    {
        public int ReturnId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Reason { get; set; }
    }
}
