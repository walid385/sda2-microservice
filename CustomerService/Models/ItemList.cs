namespace CustomerService.Models
{
    public class ItemList
    {
        public int ItemListId { get; set; } // Primary Key
        public int CartId { get; set; } // Foreign Key
        public int ProductId { get; set; } // From Inventory Service
        public int Quantity { get; set; }

        public CartInProgress Cart { get; set; }
    }
}
