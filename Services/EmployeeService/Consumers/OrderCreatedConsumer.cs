using MassTransit;
using Events;
using EmployeeService.Repositories;
using System;
using System.Threading.Tasks;

namespace EmployeeService.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public OrderCreatedConsumer(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var orderEvent = context.Message;

            // Attempt to find an employee in the same state as the customer
            var employee = await _employeeRepository.AssignEmployeeByStateAsync(orderEvent.CustomerState);

            if (employee != null)
            {
                // Save the assignment and log the result
                await _employeeRepository.SaveOrderAssignmentAsync(orderEvent.OrderId, employee.EmployeeId);
                Console.WriteLine($"Employee {employee.EmployeeId} assigned to Order {orderEvent.OrderId} for Customer {orderEvent.CustomerId} in State {orderEvent.CustomerState}");
            }
            else
            {
                Console.WriteLine($"No employee could be assigned to Order {orderEvent.OrderId} for Customer {orderEvent.CustomerId} in State {orderEvent.CustomerState}");
            }
        }
    }
}
