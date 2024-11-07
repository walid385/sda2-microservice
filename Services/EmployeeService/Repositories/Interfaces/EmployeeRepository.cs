using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeService.Models;
using EmployeeService.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _context.Employees.FindAsync(employeeId);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Employee> AssignEmployeeByStateAsync(string state)
        {
            // Try to get an employee in the specific state
            var employeeInState = await _context.Employees
                .Where(e => e.State == state)
                .OrderBy(e => e.StartDate) // Prioritize by experience (earliest start date)
                .FirstOrDefaultAsync();

            if (employeeInState != null)
            {
                return employeeInState;
            }

            // Fallback: Find the most experienced employee in any state
            return await _context.Employees
                .OrderBy(e => e.StartDate)
                .FirstOrDefaultAsync();
        }

        public async Task SaveOrderAssignmentAsync(int orderId, int employeeId)
        {
            var orderAssignment = new OrderAssignment
            {
                OrderId = orderId,
                EmployeeId = employeeId,
                AssignedDate = DateTime.UtcNow
            };

            _context.OrderAssignments.Add(orderAssignment);
            await _context.SaveChangesAsync();
        }
    }
}
