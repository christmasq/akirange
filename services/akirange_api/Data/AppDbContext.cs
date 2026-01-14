using AkiRange.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AkiRange.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
    public DbSet<PlanEntity> Plans => Set<PlanEntity>();
    public DbSet<PlannedItemEntity> PlannedItems => Set<PlannedItemEntity>();
    public DbSet<CommitMappingEntity> CommitMappings => Set<CommitMappingEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.DurationMinutes).IsRequired();
            entity.Property(e => e.CreatedAtUtc).IsRequired();
        });

        modelBuilder.Entity<PlanEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StartAtUtc).IsRequired();
            entity.Property(e => e.EndAtUtc).IsRequired();
            entity.Property(e => e.CreatedAtUtc).IsRequired();
        });

        modelBuilder.Entity<PlannedItemEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StartAtUtc).IsRequired();
            entity.Property(e => e.EndAtUtc).IsRequired();

            entity.HasOne(e => e.Plan)
                .WithMany(p => p.Items)
                .HasForeignKey(e => e.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Task)
                .WithMany(t => t.PlannedItems)
                .HasForeignKey(e => e.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CommitMappingEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.GoogleEventId).HasMaxLength(200);

            entity.HasOne(e => e.Plan)
                .WithMany(p => p.CommitMappings)
                .HasForeignKey(e => e.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Task)
                .WithMany(t => t.CommitMappings)
                .HasForeignKey(e => e.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
