using Microsoft.EntityFrameworkCore;
using Orders_Shared.Entities;

namespace Orders_Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        

        public DbSet<Country> Countries { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(i => i.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(i => i.Name).IsUnique();
        }
    }
}
