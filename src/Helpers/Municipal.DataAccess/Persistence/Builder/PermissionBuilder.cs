using Microsoft.EntityFrameworkCore;
using Municipal.Domin.Entities;

namespace Municipal.DataAccess.Persistence.Builder;

public static class PermissionBuilder
{
    public static void AddPermissionBuilder(this DbContext dbContext, ModelBuilder builder)
    {
        builder.Entity<Permission>(b =>
        {
            b.HasKey(p => p.Id);
            b.Property(p => p.Id).ValueGeneratedNever();
            b.Property(p => p.Name).IsRequired();
            b.HasOne(p => p.Role)
                .WithMany(r => r.Permissions)
                .HasForeignKey(p => p.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}