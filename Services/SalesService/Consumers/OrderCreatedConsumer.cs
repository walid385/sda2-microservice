using MassTransit;
using SalesService.Models;
using SalesService.Repositories;
using SalesService.DTOs;
using SalesService.Services;
using Events;
using System;
using System.Threading.Tasks;

namespace SalesService.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly CustomerClient _customerClient;
        private readonly TaxClient _taxClient;

        public OrderCreatedConsumer(ITicketRepository ticketRepository, CustomerClient customerClient, TaxClient taxClient)
        {
            _ticketRepository = ticketRepository;
            _customerClient = customerClient;
            _taxClient = taxClient;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var orderEvent = context.Message;

            // Fetch customer information
            var customer = await _customerClient.GetCustomerInfo(orderEvent.CustomerId);
            if (customer == null)
            {
                Console.WriteLine($"Customer with ID {orderEvent.CustomerId} not found.");
                return;
            }

            // Fetch tax rate based on customer state
            var taxRate = await _taxClient.GetTaxRateByStateAsync(customer.State);
            if (taxRate == null)
            {
                Console.WriteLine("Tax calculation failed, no default rate available.");
                return;
            }

            // Calculate tax and totals based on the subtotal from the order
            float taxAmount = orderEvent.TotalAmount * taxRate.TotalTaxRate;
            float totalWithTax = orderEvent.TotalAmount + taxAmount;

            // Create a new ticket with calculated tax and total values
            var ticket = new TicketSystem
            {
                ProductId = orderEvent.ProductId,
                CustomerId = orderEvent.CustomerId,
                Quantity = orderEvent.Quantity,
                Total = totalWithTax,
                Date = DateTime.UtcNow,
                CompanyName = "DefaultCompany",
                Time = DateTime.UtcNow.ToString("HH:mm:ss"),
                Subtotal = orderEvent.TotalAmount,
                Tax = taxAmount,
                TaxRate = taxRate.TotalTaxRate,
            };

            await _ticketRepository.AddTicketAsync(ticket);
            Console.WriteLine($"Ticket created for Product ID: {ticket.ProductId}, Customer ID: {ticket.CustomerId}, Total: {ticket.Total}, Tax: {ticket.Tax}, TaxRate: {ticket.TaxRate}");
        }


    }
}
