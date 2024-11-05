namespace CustomerService.DTOs
{
    public class ItemListDto
    {
        public int ItemListId { get; set; } 
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
