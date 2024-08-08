using FluentResults;
using Microsoft.EntityFrameworkCore;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Application.Legacy.Features.UserManagement.Permissions;
using Municipal.Application.Legacy.Features.UserManagement.Permissions.Queries.GetAllPermissions;
using Municipal.Application.Legacy.Features.UserManagement.Permissions.Queries.GetPermissionsByRoleId;
using Municipal.DataAccess.Persistence;

namespace Municipal.DataAccess.Repositories.UserManageRepo;

public class PermissionsRepository : IPermissionsRepository
{
    private readonly UserManagementDbContext _userManagementDbContext;

    public PermissionsRepository(UserManagementDbContext userManagementDbContext)
    {
        _userManagementDbContext = userManagementDbContext;
    }

    public async Task<Result<List<GetPermissionsResponse>>> GetAllPermissions(GetAllPermissionsRequest request, CancellationToken cancellationToken)
    {
        var permissions = _userManagementDbContext.Permissions
            .Include(p => p.Role)
            .GroupBy(y => y.Role)
            .Select(g => new GetPermissionsResponse()
            {
                RoleName = g.First(x => x.RoleId == x.Role.Id).Role.Name,
                Permissions = g.Select(up => new
                    Permissions()
                {
                    PermissionId = up.Id,
                    PermissionName = up.Name
                }).ToList()
            }).ToList();

        return permissions;

    }
    
    public async Task<Result<GetPermissionsResponse>> GetAllPermissionsByRoleId(GetAllPermissionsByRoleIdRequest request, CancellationToken cancellationToken)
    {
        var permissions = _userManagementDbContext.Permissions
            .Include(up => up.Role)
            .Where(up => up.RoleId == request.RoleId)
            .GroupBy(up => up.RoleId)
            .Select(g => new GetPermissionsResponse()
            {
                RoleName = g.FirstOrDefault()!.Role.Name,
                Permissions = g.Select(up => new Permissions()
                {
                    PermissionId = up.Id,
                    PermissionName = up.Name
                }).ToList()
            });
        return permissions as GetPermissionsResponse;

    }

    public async Task<Result> DeleteUserPermissions(Guid UserId, CancellationToken cancellationToken)
    {
        var permissions = await _userManagementDbContext.UserPermissions.FirstOrDefaultAsync(x => x.UserId == UserId);

        var result =  _userManagementDbContext.UserPermissions.Remove(permissions);
        await _userManagementDbContext.SaveChangesAsync();


        return Result.Ok();

    }
}