using Municipal.Domin.Models;

namespace Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.CreateUserClimas;

public class CreateUserClaimsRequest 
{
    public string UserId { get; set; }
    public List<UserClaims> Claims { get; set; }
}