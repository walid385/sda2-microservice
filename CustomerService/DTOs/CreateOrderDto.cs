namespace CustomerService.DTOs
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public float TotalAmount { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }


     public class OrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
