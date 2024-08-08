using Municipal.Domin.Models;

namespace Municipal.Application.Identity.Features.IdentityManagement.Commands.CreateUserClimas;

public class CreateUserClaimsCommand 
{
    public string UserId { get; set; }
    public List<UserClaims> Claims { get; set; }
}