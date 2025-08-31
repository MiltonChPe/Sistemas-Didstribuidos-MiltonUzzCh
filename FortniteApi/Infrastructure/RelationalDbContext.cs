using Microsoft.EntityFrameworkCore;
using FortniteApi.Infrastructure.Entities;

namespace FortniteApi.Infrastructure;

public class RelationalDbContext : DbContext
{
    public DbSet<FortniteEntity> Cosmetics { get; set; }
    public RelationalDbContext(DbContextOptions<RelationalDbContext> db) : base(db)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FortniteEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Rarity).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Price).IsRequired();
            entity.Property(e => e.Season).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Source).IsRequired().HasMaxLength(100);
        });
    }
}