using Microsoft.EntityFrameworkCore;
using TaxService.Models;
using TaxService.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaxService.Repositories
{
    public class TaxRepository : ITaxRepository
    {
        private readonly TaxContext _context;

        public TaxRepository(TaxContext context)
        {
            _context = context;
        }

        public async Task<TaxRate> GetTaxRateAsync(int year, string region = null)
        {
            return await _context.TaxRates
                .FirstOrDefaultAsync(t => t.TaxYear == year && (region == null || t.CityRate.ToString() == region));
        }

        public async Task<IEnumerable<TaxRate>> GetAllTaxRatesAsync()
        {
            return await _context.TaxRates.ToListAsync();
        }

        public async Task AddTaxRateAsync(TaxRate taxRate)
        {
            await _context.TaxRates.AddAsync(taxRate);
            await _context.SaveChangesAsync();
        }

        public async Task<TaxRate> GetTaxRateByStateAsync(string state)
        {
            var taxRate = await _context.TaxRates.FirstOrDefaultAsync(t => t.State == state);

            if (taxRate == null)
            {
                // Fetch the default rate if the specified state is not in the database
                taxRate = await _context.TaxRates.FirstOrDefaultAsync(t => t.State == "DEFAULT");
            }

            return taxRate;
        }



        public async Task UpdateTaxRateAsync(TaxRate taxRate)
        {
            _context.TaxRates.Update(taxRate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaxRateAsync(int id)
        {
            var taxRate = await _context.TaxRates.FindAsync(id);
            if (taxRate != null)
            {
                _context.TaxRates.Remove(taxRate);
                await _context.SaveChangesAsync();
            }
        }
    }
}
