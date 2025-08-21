using Microsoft.EntityFrameworkCore;
using TimeClock.Core.Models;

namespace TimeClock.Infrastructure.Data;

public class TimeClockContext : DbContext
{
    public TimeClockContext(DbContextOptions<TimeClockContext> options) : base(options)
    {
    }

    public DbSet<TimePunch> TimePunches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TimePunch>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserId).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Timestamp).IsRequired();
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).IsRequired();
            
            entity.HasIndex(e => new { e.UserId, e.Timestamp });
        });
    }
}
