using SalesService.Models;

namespace SalesService.Repositories
{
    public interface ITicketRepository
    {
        Task<IEnumerable<TicketSystem>> GetAllTicketsAsync();
        Task<TicketSystem> GetTicketByIdAsync(int ticketId);
        Task AddTicketAsync(TicketSystem ticket);
        Task UpdateTicketAsync(TicketSystem ticket);
        Task DeleteTicketAsync(int ticketId);
    }
}
