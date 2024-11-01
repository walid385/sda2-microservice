namespace OrderManagementService.Events
{
   public class OrderUpdatedEvent
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
}

}
