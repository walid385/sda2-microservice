namespace Events
{
    public interface ILowStockEvent
    {
        int ProductId { get; }
        int Quantity { get; }
    }
}
