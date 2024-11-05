using MassTransit;
using Events;
using SalesService.Models;
using SalesService.Repositories;
using System;
using System.Threading.Tasks;

namespace SalesService.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ITicketRepository _ticketRepository;

        public OrderCreatedConsumer(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var orderEvent = context.Message;

            // Create a ticket based on the single product in the event
            var ticket = new TicketSystem
            {

                ProductId = orderEvent.ProductId,
                CustomerId = orderEvent.CustomerId,
                Quantity = orderEvent.Quantity,
                Total = orderEvent.TotalAmount,
                Date = DateTime.UtcNow,
                CompanyName = "DefaultCompany", 
                Time = DateTime.UtcNow.ToString("HH:mm:ss"),  
                Subtotal = orderEvent.TotalAmount * 0.9m 
                
            };

            await _ticketRepository.AddTicketAsync(ticket);
            Console.WriteLine($"Ticket created for Product ID: {ticket.ProductId}, Order ID: {orderEvent.OrderId}");
        }
    }
}
