using MassTransit;
using SalesService.Events;
using SalesService.Models;
using SalesService.Repositories;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace SalesService.Consumers
{
    public class OrderEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public OrderEventConsumer(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var orderEvent = context.Message;

            // Process payment
            bool paymentSuccess = ProcessPayment(orderEvent);

            if (paymentSuccess)
            {
                // Create a sales ticket if payment is successful
                var ticket = new TicketSystem
                {
                    ProductId = orderEvent.ProductId,  // Assumes this property exists in OrderCreatedEvent
                    Date = DateTime.Now,
                    CompanyName = "YourCompanyName",  // Adjust if needed
                    Time = DateTime.Now.ToString("HH:mm:ss"),
                    Quantity = orderEvent.Quantity,
                    Subtotal = orderEvent.Total * 0.9f, // Example calculation, adjust as needed
                    Total = orderEvent.Total,
                    CustomerId = orderEvent.CustomerId,
                    EmployeeId = 1 // Assume an employee ID if not provided, adjust as needed
                };

                await _ticketRepository.AddTicketAsync(ticket);
                Console.WriteLine($"Sales ticket created for Order ID: {orderEvent.OrderId}");
            }
            else
            {
                Console.WriteLine($"Payment failed for Order ID: {orderEvent.OrderId}");
            }
        }

        private bool ProcessPayment(OrderCreatedEvent orderEvent)
        {
            // Simulated payment processing logic
            Console.WriteLine($"Processing payment for Order ID: {orderEvent.OrderId}, Amount: {orderEvent.Total}");
            return true; // Simulate successful payment
        }
    }
}
