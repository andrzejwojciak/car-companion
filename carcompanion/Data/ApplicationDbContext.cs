using carcompanion.Models;
using Microsoft.EntityFrameworkCore;

namespace carcompanion.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Test> Tests { get; set; }
        
    }
}