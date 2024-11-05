using Microsoft.EntityFrameworkCore;
using SalesService.Models;

namespace SalesService.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options)
            : base(options)
        {
        }

        public DbSet<TicketSystem> Tickets { get; set; }
        public DbSet<RegistersTable> Registers { get; set; }
    }
}
