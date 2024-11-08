using MassTransit;
using Events;
using OrderManagementService.Models;
using OrderManagementService.Repositories;
using System;
using System.Threading.Tasks;

namespace OrderManagementService.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderCreatedConsumer(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var orderEvent = context.Message;

            // Log received event information
            Console.WriteLine($"Received OrderCreatedEvent for ProductId: {orderEvent.ProductId}, Total: {orderEvent.TotalAmount}");

            // Create a new order based on the event data
            var order = new Order
            { 
                ProductId = orderEvent.ProductId,
                CustomerId = orderEvent.CustomerId,
                Quantity = orderEvent.Quantity,
                TotalAmount = orderEvent.TotalAmount,
                OrderDate = orderEvent.OrderDate,
            };

            // Save the order in the database
            await _orderRepository.AddOrderAsync(order);

            // Log the generated OrderId after saving
            Console.WriteLine($"Order created for Product ID: {order.ProductId}, Order ID: {order.OrderId}, Total: {order.TotalAmount}");
        }
    }
}
