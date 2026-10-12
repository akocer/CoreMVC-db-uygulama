using Microsoft.EntityFrameworkCore;

namespace uy01.Models
{
    public class AppDbContext:DbContext
    {
       
       public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
    
    
}
