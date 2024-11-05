namespace InventoryService.Events
{
    public class LowStockEvent
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
