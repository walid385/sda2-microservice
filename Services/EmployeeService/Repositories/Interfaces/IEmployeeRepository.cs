using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeService.Models;

namespace EmployeeService.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int employeeId);
        Task<Employee> AssignEmployeeByStateAsync(string state);
        Task SaveOrderAssignmentAsync(int orderId, int employeeId);
    }
}
