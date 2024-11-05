namespace OrderManagementService.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}