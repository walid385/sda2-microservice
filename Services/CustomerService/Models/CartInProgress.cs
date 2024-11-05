
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Models
{
    public class CartInProgress
    {

        public int CartId { get; set; } // Primary Key
        public int CustomerId { get; set; } // Foreign Key
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public CustomerInfo? Customer { get; set; }
        public ICollection<ItemList>? ItemLists { get; set; }
    }
}
