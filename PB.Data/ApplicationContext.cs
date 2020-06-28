using Microsoft.EntityFrameworkCore;
using PB.Data.Entities;

namespace PB.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().HasKey(c => c.Id);
            modelBuilder.Entity<Product>().Property(c => c.Id).HasColumnType("bigint").ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().Property(c => c.Name).HasMaxLength(200);
            modelBuilder.Entity<Product>().Property(c => c.Description).IsRequired(false).HasMaxLength(500);
        }
    }
}
