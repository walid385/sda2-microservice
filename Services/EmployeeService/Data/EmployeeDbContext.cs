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
            // Unique constraint on Email in the Employee entity
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

            // Foreign key relationship between OrderAssignment and Employee
            modelBuilder.Entity<OrderAssignment>()
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(oa => oa.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if linked to OrderAssignment

            base.OnModelCreating(modelBuilder);
        }
    }
}
