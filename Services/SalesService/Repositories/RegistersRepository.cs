using System.Collections.Generic;
using System.Threading.Tasks;
using SalesService.Models;
using SalesService.Data;
using Microsoft.EntityFrameworkCore;

namespace SalesService.Repositories
{
    public class RegistersRepository : IRegistersRepository
    {
        private readonly SalesContext _context;

        public RegistersRepository(SalesContext context)
        {
            _context = context;
        }

        public async Task<RegistersTable> GetRegisterByIdAsync(int registerId)
        {
            return await _context.Registers.FindAsync(registerId);
        }

        public async Task<IEnumerable<RegistersTable>> GetAllRegistersAsync()
        {
            return await _context.Registers.ToListAsync();
        }

        public async Task OpenRegisterAsync(RegistersTable register)
        {
            _context.Registers.Add(register);
            await _context.SaveChangesAsync();
        }

        public async Task CloseRegisterAsync(RegistersTable register)
        {
            _context.Registers.Update(register);
            await _context.SaveChangesAsync();
        }
    }
}