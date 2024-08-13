using Municipal.Domin.Models;

namespace Municipal.Application.Legacy.Models.IdentityModel;

public class InsertAndUpdateIdentityClaims
{
    public string UserId { get; set; }
    public List<UserClaims> Claims { get; set; }
}