namespace Events
{
    public class OrderCreatedEvent
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalAmount { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

