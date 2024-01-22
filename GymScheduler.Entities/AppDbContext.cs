using GymScheduler.Entities.Entities;
using GymScheduler.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GymScheduler.Entities;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Member> Members => base.Set<Member>();
    public DbSet<Training> Trainings => base.Set<Training>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var member = modelBuilder.Entity<Member>();
        {
            member.HasKey(x => x.Id);
            member.Property(x => x.Id)
                .HasConversion(
                    x => x.Value,
                    x => new MemberId(x));
        }

        var training = modelBuilder.Entity<Training>();
        {
            training.HasKey(x => x.Id);
            training.Property(x => x.Id)
                .HasConversion(
                    x => x.Value,
                    x => new TrainingId(x));
            training.Property(x => x.MemberId)
                .HasConversion(
                    x => x.Value,
                    x => new MemberId(x));
        }
    }

    public override Int32 SaveChanges()
    {
        SetDates();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetDates();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SetDates()
    {
        var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is EntityBase && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));
        foreach (var entityEntry in entries)
        {
            ((EntityBase)entityEntry.Entity).Updated = DateTime.Now;
            if (entityEntry.State == EntityState.Added)
            {
                ((EntityBase)entityEntry.Entity).Created = DateTime.Now;
            }
        }
    }
}
