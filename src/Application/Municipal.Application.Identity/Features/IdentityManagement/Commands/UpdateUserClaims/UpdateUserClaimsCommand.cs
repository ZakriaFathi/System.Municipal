using Municipal.Domin.Models;

namespace Municipal.Application.Identity.Features.IdentityManagement.Commands.UpdateUserClaims;

public class UpdateUserClaimsCommand
{
    public string UserId { get; set; }
    public List<UserClaims> Claims { get; set; }
}