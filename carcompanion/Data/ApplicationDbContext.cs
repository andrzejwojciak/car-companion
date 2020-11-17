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
        
    }
}