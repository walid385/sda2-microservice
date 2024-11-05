using Microsoft.EntityFrameworkCore;
using TaxService.Models;

namespace TaxService.Data
{
    public class TaxContext : DbContext
    {
        public TaxContext(DbContextOptions<TaxContext> options) : base(options) { }

        public DbSet<TaxRate> TaxRates { get; set; }
    }
}
