namespace CustomerService.DTOs
{
    public class CartInProgressDto
    {
        public int CartId { get; set; } 
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ItemListDto> ItemLists { get; set; }
    }
}
