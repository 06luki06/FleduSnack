using At.luki0606.FleduSnack.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace At.Luki0606.FleduSnack.Server.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cat> Cats => Set<Cat>();
        public DbSet<Dish> Dishes => Set<Dish>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cat>(e =>
            {
                e.HasKey(c => c.Id);
                e.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Dish>(e =>
            {
                e.HasKey(d => d.Id);
                e.Property(d => d.Brand).IsRequired().HasMaxLength(100);
                e.Property(d => d.Flavor).HasMaxLength(100);
                e.Property(d => d.PhotoPath).HasMaxLength(500);
                e.Property(d => d.Tasting).HasConversion<string>().IsRequired();

                e.HasOne<Cat>()
                .WithMany()
                .HasForeignKey(d => d.CatId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
