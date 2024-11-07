using SalesService.Models;

namespace SalesService.Repositories
{
    public interface IRegistersRepository
    {
        Task<RegistersTable> GetRegisterByIdAsync(int registerId);
        Task<IEnumerable<RegistersTable>> GetAllRegistersAsync();
        Task OpenRegisterAsync(RegistersTable register);
        Task CloseRegisterAsync(RegistersTable register);
    }
}
