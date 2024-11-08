using SalesService.Data;
using SalesService.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesService.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly SalesContext _context;

        public TicketRepository(SalesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TicketSystem>> GetAllTicketsAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<TicketSystem> GetTicketByIdAsync(int ticketId)
        {
            return await _context.Tickets.FindAsync(ticketId);
        }

        public async Task AddTicketAsync(TicketSystem ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }
        

        public async Task UpdateTicketAsync(TicketSystem ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(int ticketId)
        {
            var ticket = await GetTicketByIdAsync(ticketId);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}
