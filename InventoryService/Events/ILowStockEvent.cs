namespace InventoryService.Events
{
    public interface ILowStockEvent
    {
        int ProductId { get; }
        int Quantity { get; }
    }
}
