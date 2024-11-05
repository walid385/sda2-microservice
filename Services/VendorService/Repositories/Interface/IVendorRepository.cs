using VendorService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VendorService.Repositories
{
    public interface IVendorRepository
    {
        Task<IEnumerable<Vendor>> GetAllVendorsAsync();
        Task<Vendor> GetVendorByIdAsync(int id);
        Task AddVendorAsync(Vendor vendor);
        Task UpdateVendorAsync(Vendor vendor);
        Task DeleteVendorAsync(int id);
    }
}
