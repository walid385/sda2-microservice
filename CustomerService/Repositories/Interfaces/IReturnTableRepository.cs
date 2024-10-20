using CustomerService.Models;

namespace CustomerService.Repositories
{
    public interface IReturnTableRepository
    {
        Task<IEnumerable<ReturnTable>> GetReturnsByCustomerIdAsync(int customerId);
        Task<ReturnTable> GetReturnByIdAsync(int returnId);
        Task AddReturnAsync(ReturnTable returnTable);
        Task UpdateReturnAsync(ReturnTable returnTable);
        Task DeleteReturnAsync(int returnId);
    }
}
