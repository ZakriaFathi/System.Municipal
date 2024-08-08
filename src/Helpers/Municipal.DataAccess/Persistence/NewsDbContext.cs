using Microsoft.EntityFrameworkCore;
using Municipal.Domin.Common;
using Municipal.Domin.Entities;

namespace Municipal.DataAccess.Persistence;

public class NewsDbContext : DbContext
{

    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }


public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
{
}
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(NewsDbContext).Assembly);
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