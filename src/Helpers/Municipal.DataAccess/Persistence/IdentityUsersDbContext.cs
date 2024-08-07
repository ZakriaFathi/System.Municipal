using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Municipal.Domin.Models;

namespace Municipal.DataAccess.Persistence;

public class IdentityUsersDbContext : IdentityDbContext<AppUser>
{
    public IdentityUsersDbContext(DbContextOptions<IdentityUsersDbContext> Options) : base(Options) { }
}