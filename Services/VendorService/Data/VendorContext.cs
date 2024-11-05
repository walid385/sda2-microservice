using Microsoft.EntityFrameworkCore;
using VendorService.Models;

namespace VendorService.Data
{

public class VendorContext : DbContext
{
    public VendorContext(DbContextOptions<VendorContext> options) : base(options) { }

    public DbSet<Vendor> Vendors { get; set; }
}

}
