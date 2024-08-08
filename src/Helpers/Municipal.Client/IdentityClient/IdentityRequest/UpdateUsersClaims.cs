using System.Security.Claims;
using Municipal.Application.Legacy.Features.UserManagement.Permissions;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUserPermissions;
using Municipal.Domin.Models;

namespace Municipal.Client.IdentityClient.IdentityRequest;

public class UpdateUsersClaims
{
    public string UserId { get; set; }
    public List<UserClaims> Claims { get; set; }

    public static UpdateUsersClaims Prepare(UpdateUserPermissionsRequest request, List<GetPermissionsResponse> allPermissions)
    {
        var Claims = new List<Claim>();
        foreach (var t in allPermissions)
        {
            t.Permissions.ForEach(x =>
            {
                request.Permissions.ForEach(y =>
                {
                    if (y.ToString() == x.PermissionId.ToString())
                        Claims.Add(new Claim(x.PermissionName, t.RoleName));
                });
            });
        }
        var userClaims = Claims.GroupBy(x => x.Type).Select(claim => new UserClaims
        {
            type = claim.Key,
            value = claim.Select(x => x.Value).ToList()
        }).ToList();

        return new UpdateUsersClaims
        {
            UserId = request.UserId.ToString(),
            Claims = userClaims
        };
    }
}