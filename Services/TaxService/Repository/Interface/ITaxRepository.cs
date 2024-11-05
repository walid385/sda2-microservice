using TaxService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaxService.Repositories
{
    public interface ITaxRepository
    {
        Task<TaxRate> GetTaxRateAsync(int year, string region = null);
        Task<IEnumerable<TaxRate>> GetAllTaxRatesAsync();
        Task<TaxRate> GetTaxRateByStateAsync(string state);
        Task AddTaxRateAsync(TaxRate taxRate);
        Task UpdateTaxRateAsync(TaxRate taxRate);
        Task DeleteTaxRateAsync(int id);
    }
}
