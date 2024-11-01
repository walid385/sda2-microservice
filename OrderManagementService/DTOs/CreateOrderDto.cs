namespace OrderManagementService.DTOs
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

}