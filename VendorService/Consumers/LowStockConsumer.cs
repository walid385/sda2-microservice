using MassTransit;
using System;
using System.Threading.Tasks;
using VendorService.Events;

namespace VendorService.Consumers
{
    public class LowStockConsumer : IConsumer<LowStockEvent>
    {
        public async Task Consume(ConsumeContext<LowStockEvent> context)
        {
            var lowStockEvent = context.Message;

            Console.WriteLine($"[x] Received LowStockEvent - Product ID: {lowStockEvent.ProductId}, Quantity: {lowStockEvent.Quantity}");

            // Handle the low stock event, e.g., place an order to restock the product
            await Task.CompletedTask;
        }
    }
}

