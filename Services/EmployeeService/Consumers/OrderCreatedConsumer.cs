using MassTransit;
using Events;
using EmployeeService.Repositories;
using EmployeeService.Services;
using System;
using System.Threading.Tasks;

namespace EmployeeService.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly CustomerClient _customerClient;
        private readonly IEmployeeRepository _employeeRepository;

        public OrderCreatedConsumer(CustomerClient customerClient, IEmployeeRepository employeeRepository)
        {
            _customerClient = customerClient;
            _employeeRepository = employeeRepository;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var orderEvent = context.Message;

            // Log received OrderId and CustomerId
            Console.WriteLine($"Received OrderCreatedEvent with OrderId: {orderEvent.OrderId}, CustomerId: {orderEvent.CustomerId}");

            // Fetch additional data needed
            var customer = await _customerClient.GetCustomerInfo(orderEvent.CustomerId);
            if (customer == null)
            {
                Console.WriteLine($"Customer with ID {orderEvent.CustomerId} not found.");
                return;
            }

            var state = customer.State;

            // Proceed with employee assignment using the fetched state
            var employee = await _employeeRepository.AssignEmployeeByStateAsync(state);

            if (employee != null)
            {
                await _employeeRepository.SaveOrderAssignmentAsync(orderEvent.OrderId, employee.EmployeeId);
                Console.WriteLine($"Employee {employee.EmployeeId} assigned for Customer {orderEvent.CustomerId} in State {state}");
            }
            else
            {
                Console.WriteLine($"No employee could be assigned for Customer {orderEvent.CustomerId} in State {state}");
            }
        }
    }


}
