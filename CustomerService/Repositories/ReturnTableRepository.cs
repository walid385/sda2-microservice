using CustomerService.Data;
using CustomerService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Repositories
{
    public class ReturnTableRepository : IReturnTableRepository
    {
        private readonly CustomerContext _context;

        public ReturnTableRepository(CustomerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReturnTable>> GetReturnsByCustomerIdAsync(int customerId)
        {
            return await _context.ReturnTables
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<ReturnTable> GetReturnByIdAsync(int returnId)
        {
            return await _context.ReturnTables.FindAsync(returnId);
        }

        public async Task AddReturnAsync(ReturnTable returnTable)
        {
            await _context.ReturnTables.AddAsync(returnTable);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReturnAsync(ReturnTable returnTable)
        {
            _context.ReturnTables.Update(returnTable);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReturnAsync(int returnId)
        {
            var returnTable = await GetReturnByIdAsync(returnId);
            if (returnTable != null)
            {
                _context.ReturnTables.Remove(returnTable);
                await _context.SaveChangesAsync();
            }
        }
    }
}
