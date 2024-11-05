using MassTransit;
using Events;

namespace VendorService.Consumers
{
    public class LowStockConsumer : IConsumer<ILowStockEvent>
    {
        public async Task Consume(ConsumeContext<ILowStockEvent> context)
        {
            // Logic for handling the low stock event
            var productId = context.Message.ProductId;
            var quantity = context.Message.Quantity;

            // Example: Log the received message
            Console.WriteLine($"Low stock alert received for ProductId: {productId}, Quantity: {quantity}");
        }
    }
}
