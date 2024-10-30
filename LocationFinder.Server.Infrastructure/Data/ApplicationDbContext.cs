using LocationFinder.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocationFinder.Server.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Location> Locations { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
                    .HasOne(c => c.Parent)
                    .WithMany(c => c.Subcategories)
                    .HasForeignKey(c => c.ParentId)
                    .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete
        modelBuilder.Entity<Location>()
                    .HasOne(l => l.Category);

        base.OnModelCreating(modelBuilder);
    }
}