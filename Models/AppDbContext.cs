using Microsoft.EntityFrameworkCore;

namespace Personal_Finance_Manager.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            Database.EnsureCreated(); // Creating DB for the first time
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id_category = 1, Name = "Food", Description = "Food cost"}
            );
        }
    }
}
