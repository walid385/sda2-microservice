using MassTransit;
using InventoryService.Events;
using System.Threading.Tasks;

namespace InventoryService.Publisher
{
    public class LowStockEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public LowStockEventPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishLowStockEvent(int productId, int quantity)
        {
            var lowStockEvent = new LowStockEvent
            {
                ProductId = productId,
                Quantity = quantity
            };

            await _publishEndpoint.Publish(lowStockEvent);
            Console.WriteLine(" [x] Published LowStockEvent to exchange 'low_stock_exchange'");
        }
    }
}
