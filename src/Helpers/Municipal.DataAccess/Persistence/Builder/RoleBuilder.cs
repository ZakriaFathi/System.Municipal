using Microsoft.EntityFrameworkCore;
using Municipal.Domin.Entities;

namespace Municipal.DataAccess.Persistence.Builder;

public static class RoleBuilder
{
    public static void AddRoleBuilder(this DbContext dbContext, ModelBuilder builder)
    {
        builder.Entity<Role>(b =>
        {
            b.HasKey(r => r.Id);
            b.Property(r => r.Id).ValueGeneratedNever();
            b.Property(r => r.Name).IsRequired();
            b.HasMany(r => r.Permissions)
                .WithOne()
                .HasForeignKey(p => p.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}