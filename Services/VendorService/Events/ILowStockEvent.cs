namespace VendorService.Events
{
    public interface ILowStockEvent
    {
        string ProductId { get; }
        int Quantity { get; }
    }
}
