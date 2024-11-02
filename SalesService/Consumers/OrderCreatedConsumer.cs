using MassTransit.RabbitMqTransport;
using MassTransit;
using SalesService.Events;
using SalesService.Models;
using SalesService.Repositories;
using System;
using System.Threading.Tasks;

namespace SalesService.Consumers
{
    public class OrderEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ITicketRepository _ticketRepository;

        public OrderEventConsumer(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var orderEvent = context.Message;
            Console.WriteLine($"Received OrderCreatedEvent: Order ID = {orderEvent.OrderId}, Product ID = {orderEvent.ProductId}");

            // Additional logging
            Console.WriteLine("Processing the ticket creation...");

            bool paymentSuccess = ProcessPayment(orderEvent);
            if (!paymentSuccess)
            {
                Console.WriteLine($"Payment failed for Order ID: {orderEvent.OrderId}");
                return;
            }

            var ticket = new TicketSystem
            {
                ProductId = orderEvent.ProductId,
                Date = DateTime.Now,
                CompanyName = "YourCompanyName",
                Time = DateTime.Now.ToString("HH:mm:ss"),
                Quantity = orderEvent.Quantity,
                Subtotal = orderEvent.Total * 0.9f,
                Total = orderEvent.Total,
                CustomerId = orderEvent.CustomerId,
                EmployeeId = 1
            };

            await _ticketRepository.AddTicketAsync(ticket);
            Console.WriteLine($"Sales ticket created for Order ID: {orderEvent.OrderId}");
        }

        private bool ProcessPayment(OrderCreatedEvent orderEvent)
        {
            Console.WriteLine($"Processing payment for Order ID: {orderEvent.OrderId}, Amount: {orderEvent.Total}");
            return true;
        }
    }
}
