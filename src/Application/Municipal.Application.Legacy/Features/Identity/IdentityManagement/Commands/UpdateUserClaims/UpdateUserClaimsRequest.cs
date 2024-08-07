using Municipal.Domin.Models;

namespace Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.UpdateUserClaims;

public class UpdateUserClaimsRequest
{
    public string UserId { get; set; }
    public List<UserClaims> Claims { get; set; }
}