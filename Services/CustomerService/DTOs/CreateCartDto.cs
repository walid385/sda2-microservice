namespace CustomerService.DTOs
{
    public class CreateCartDto
    {
        public int CustomerId { get; set; } 
        public List<CreateItemDto> Items { get; set; } 
    }

    public class CreateItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
