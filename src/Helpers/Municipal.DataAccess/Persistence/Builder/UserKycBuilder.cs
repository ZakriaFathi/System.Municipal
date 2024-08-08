using Microsoft.EntityFrameworkCore;
using Municipal.Domin.Entities;

namespace Municipal.DataAccess.Persistence.Builder;

public static class UserKycBuilder
{
    public static void AddUserKycBuilder(this DbContext dbContext, ModelBuilder builder)
    {
        builder.Entity<UserKyc>(b =>
        {
            b.HasKey(e => e.Id);
            b.Property(e => e.FirstName).IsRequired();
            b.Property(e => e.FatherName).IsRequired();
            b.Property(e => e.LastName).IsRequired();
            b.Property(e => e.Nationality).IsRequired();
            b.Property(e => e.Country).IsRequired();
            b.Property(e => e.DateOfBirth).IsRequired();
            b.Property(e => e.Gender).IsRequired().HasMaxLength(10);
            b.Property(e => e.NationalId).IsRequired();
            b.Property(e => e.PassportId).IsRequired();
            b.Property(e => e.PassportExpirationDate).IsRequired();
            b.Property(e => e.placeOfIssue).IsRequired();
            b.Property(e => e.UserId).IsRequired();
            b.HasOne<User>()
                .WithOne()
                .HasForeignKey<UserKyc>(e => e.UserId)
                .IsRequired();
        });
        
    }
}