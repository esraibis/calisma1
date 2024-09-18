using Microsoft.EntityFrameworkCore;
using calisma1.Models;

namespace calisma1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Point> Points { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Ek yapılandırmalar gerekiyorsa burada yapılabilir
        }
    }
}