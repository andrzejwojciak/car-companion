using Microsoft.EntityFrameworkCore;
using carcompanion.Models;

namespace carcompanion.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserCar> UserCars { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<ShareKey> ShareKeys { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserCarRole> UserCarRoles { get; set; }
        public DbSet<Log> Logs { get; set; }

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
                .HasKey(uc => new {uc.CarId, uc.UserId});

            modelBuilder.Entity<UserCar>()
                .HasOne(r => r.UserCarRole)
                .WithMany(u => u.UserCars)
                .HasForeignKey(r => r.UserCarRoleId);

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

            modelBuilder.Entity<ShareKey>()
                .HasOne(i => i.Issuer)
                .WithMany(s => s.ShareKeys)
                .HasForeignKey(i => i.IssuerId);

            modelBuilder.Entity<ShareKey>()
                .HasOne(i => i.Car)
                .WithMany(s => s.ShareKeys)
                .HasForeignKey(i => i.CarId);

            modelBuilder.Entity<ShareKey>()
                .HasOne(c => c.UserCarRole)
                .WithMany(s => s.ShareKeys)
                .HasForeignKey(c => c.UserCarRoleId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(k => k.RoleId);

            modelBuilder.Seed();
        }
    }
}