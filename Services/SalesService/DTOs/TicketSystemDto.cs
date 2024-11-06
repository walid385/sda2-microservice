namespace SalesService.DTOs
{
    public class TicketSystemDto
    {
        
        public int TicketId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public float Total { get; set; }
        public DateTime Date { get; set; }
        public string CompanyName { get; set; }
        public string Time { get; set; }
        public float? Subtotal { get; set; }
        public float? Cost { get; set; }
        public float? Discount { get; set; }
        public float? Tax { get; set; }
        public float? TaxRate { get; set; }
        public float? Cash { get; set; }
        public float? Credit { get; set; }
        public bool? CartPurchase { get; set; }
        public int? EmployeeId { get; set; }
    }
}