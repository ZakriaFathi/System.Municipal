using Microsoft.EntityFrameworkCore;
using Municipal.Domin.Entities;

namespace Municipal.DataAccess.Persistence.Builder;

public static class UserPermissionsBuilder
{
    public static void AddUserPermissionsBuilder(this DbContext dbContext, ModelBuilder builder)
    {
        builder.Entity<UserPermission>(b =>
        {
            b.HasKey(up => new { up.UserId, up.PermissionId });
            b.HasOne(up => up.User)
                .WithMany(u => u.UserPermissions)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(up => up.Permission)
                .WithMany(p => p.UserPermissions)
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // b.Property(up => up.ReviewState).IsRequired();
            // b.Property(up => up.AllowedBy).IsRequired();
        });
    }
}