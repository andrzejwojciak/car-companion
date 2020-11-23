using Microsoft.EntityFrameworkCore;
using carcompanion.Models;

namespace carcompanion.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        
        public DbSet<Car> Cars { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
            modelBuilder.Entity<Car>()
                .HasMany(c => c.Expenses)
                .WithOne(e => e.Car);
            
            modelBuilder.Entity<Expense>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");
        }
        
    }
}