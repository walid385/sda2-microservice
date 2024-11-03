using MassTransit;
using VendorService.Events;
using System.Threading.Tasks;

namespace VendorService.Consumers
{
    public class LowStockConsumer : IConsumer<LowStockEvent>
    {
        public async Task Consume(ConsumeContext<LowStockEvent> context)
        {
            var lowStockEvent = context.Message;

            // Log or handle the low stock event
            Console.WriteLine($"Low stock alert received for Product ID: {lowStockEvent.ProductId}, Quantity: {lowStockEvent.Quantity}");

            // Here, you could place logic to create an order with the vendor to restock the product
            // This could involve calling the repository or another service to place the order
        }
    }
}
