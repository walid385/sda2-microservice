namespace SalesService.DTOs
{
    public class TicketSystemDto
    {
        public int TicketId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public string CompanyName { get; set; }
        public string Time { get; set; }
        public int Quantity { get; set; }
        public float Subtotal { get; set; }
        public float Total { get; set; }
        public float Discount { get; set; }
        public float Tax { get; set; }
    }
}