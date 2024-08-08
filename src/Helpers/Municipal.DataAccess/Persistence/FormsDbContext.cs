using Microsoft.EntityFrameworkCore;
using Municipal.Domin.Common;
using Municipal.Domin.Entities;

namespace Municipal.DataAccess.Persistence;

public class FormsDbContext : DbContext
{
    public DbSet<Form> Forms { get; set; }


    public FormsDbContext(DbContextOptions<FormsDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FormsDbContext).Assembly);
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedBy = "";
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.Now;
                    entry.Entity.UpdatedBy = "";
                    break;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }

}