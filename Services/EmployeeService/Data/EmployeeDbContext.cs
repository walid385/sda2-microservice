using Microsoft.EntityFrameworkCore;
using EmployeeService.Models;

namespace EmployeeService.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<OrderAssignment> OrderAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<OrderAssignment>()
                .HasOne(oa => oa.Employee) 
                .WithMany() 
                .HasForeignKey(oa => oa.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict); 

            base.OnModelCreating(modelBuilder);
        }
    }
}

