using CustomerService.Models;


namespace CustomerService.Repositories
{
    public interface ICustomerRepository
    {
        // CustomerInfo methods
        Task<IEnumerable<CustomerInfo>> GetAllCustomersAsync();
        Task<CustomerInfo> GetCustomerByIdAsync(int customerId);
        Task AddCustomerAsync(CustomerInfo customer);
        Task UpdateCustomerAsync(CustomerInfo customer);
        Task DeleteCustomerAsync(int customerId);

    }
}
