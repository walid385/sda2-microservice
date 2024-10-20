using CustomerService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerInfo> Customers { get; set; }
        public DbSet<CartInProgress> CartsInProgress { get; set; }
        public DbSet<ItemList> ItemLists { get; set; }
        public DbSet<GiftCard> GiftCards { get; set; }
        public DbSet<ReturnTable> ReturnTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicit primary key configuration for each entity
            modelBuilder.Entity<CustomerInfo>()
                .HasKey(c => c.CustomerId);  // Primary key for CustomerInfo

            modelBuilder.Entity<CartInProgress>()
                .HasKey(c => c.CartId);  // Primary key for CartInProgress

            modelBuilder.Entity<ItemList>()
                .HasKey(i => i.ItemListId);  // Primary key for ItemList

            modelBuilder.Entity<GiftCard>()
                .HasKey(g => g.GiftCardId);  // Primary key for GiftCard

            modelBuilder.Entity<ReturnTable>()
                .HasKey(r => r.ReturnId);  // Primary key for ReturnTable

            // Relationships between CustomerInfo and related entities
            modelBuilder.Entity<CustomerInfo>()
                .HasMany(c => c.CartsInProgress)
                .WithOne(c => c.Customer)
                .HasForeignKey(c => c.CustomerId);

            modelBuilder.Entity<CustomerInfo>()
                .HasMany(c => c.GiftCards)
                .WithOne(g => g.Customer)
                .HasForeignKey(g => g.CustomerId);

            modelBuilder.Entity<CustomerInfo>()
                .HasMany(c => c.Returns)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId);

            // Relationships between CartInProgress and ItemList
            modelBuilder.Entity<CartInProgress>()
                .HasMany(c => c.ItemLists)
                .WithOne(i => i.Cart)
                .HasForeignKey(i => i.CartId);
        }
    }
}
