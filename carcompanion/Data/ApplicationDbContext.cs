using Microsoft.EntityFrameworkCore;
using carcompanion.Models;

namespace carcompanion.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserCar> UserCars { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<UserCar>()
                .HasOne(u => u.User)                     
                .WithMany(u => u.UserCars)
                .HasForeignKey(i => i.UserId);
                            
            modelBuilder.Entity<UserCar>()
                .HasOne(u => u.Car)                     
                .WithMany(u => u.UserCars)
                .HasForeignKey(i => i.CarId);
                
            modelBuilder.Entity<UserCar>()
                .HasKey(uc => new { uc.CarId, uc.UserId});            
            
            modelBuilder.Entity<Expense>()
                .Property(p => p.Amount) 
                .HasColumnType("decimal(18,2)");
            
            modelBuilder.Entity<Expense>()
                .HasOne(u => u.User)
                .WithMany(u => u.Expenses)
                .HasForeignKey(p => p.UserId);
            
            modelBuilder.Entity<Expense>()
                .HasOne(u => u.Car)
                .WithMany(u => u.Expenses)
                .HasForeignKey(p => p.CarId);
        }
        
    }
}