using MassTransit;
using InventoryService.Events;
using System.Threading.Tasks;

namespace InventoryService.Consumers
{
    public class TemporaryLowStockConsumer : IConsumer<LowStockEvent>
    {
        public async Task Consume(ConsumeContext<LowStockEvent> context)
        {
            var message = context.Message;
            Console.WriteLine($"[Temporary Consumer] Low stock alert received for Product {message.ProductId}. Quantity: {message.Quantity}");
            // You can add more logic here if needed for testing purposes
        }
    }
}
