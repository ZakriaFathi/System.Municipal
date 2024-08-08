using Microsoft.EntityFrameworkCore;
using Municipal.Domin.Entities;

namespace Municipal.DataAccess.Persistence.Builder;

public static class UserBuilder
{
    public static void AddUserBuilder(this DbContext dbContext, ModelBuilder builder)
    {
        builder.Entity<User>(b =>
        {
            b.HasKey(u => u.Id);
            b.Property(u => u.UserName).IsRequired();
            b.Property(u => u.PhoneNumber).IsRequired();
            b.Property(u => u.Address).IsRequired();
            b.Property(u => u.Email);
            b.Property(u => u.Password).IsRequired();
            b.Property(u => u.ActivateState).IsRequired();
            b.Property(u => u.UserType).IsRequired();
            b.Property(u => u.CreatedAt).IsRequired();
            b.Property(u => u.CreatedBy).IsRequired();

            b.HasMany(u => u.UserPermissions)
                .WithOne(up => up.User)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}