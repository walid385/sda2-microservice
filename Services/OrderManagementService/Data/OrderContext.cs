using Microsoft.EntityFrameworkCore;
using OrderManagementService.Models;

namespace OrderManagementService.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
    }
}
