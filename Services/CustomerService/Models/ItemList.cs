namespace CustomerService.Models
{
    public class ItemList
    {
        public int ItemListId { get; set; } 
        public int CartId { get; set; } 
        public int ProductId { get; set; } 
        public int Quantity { get; set; }
        public float UnitPrice { get; set; } 
        public CartInProgress? Cart { get; set; }
    }
}
