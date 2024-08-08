using Microsoft.EntityFrameworkCore;
using Municipal.DataAccess.Persistence.Builder;
using Municipal.Domin.Common;
using Municipal.Domin.Entities;

namespace Municipal.DataAccess.Persistence;

public class UserManagementDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserKyc> UsersKyc { get; set; } = null!;
    public DbSet<Permission> Permissions { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<UserPermission> UserPermissions { get; set; } = null!;

    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        this.AddUserBuilder(modelBuilder);
        this.AddRoleBuilder(modelBuilder);
        this.AddPermissionBuilder(modelBuilder);
        this.AddUserPermissionsBuilder(modelBuilder);
        base.OnModelCreating(modelBuilder);
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