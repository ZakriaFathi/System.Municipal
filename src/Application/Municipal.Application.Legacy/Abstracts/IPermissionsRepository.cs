using FluentResults;
using Municipal.Application.Legacy.Features.UserManagement.Permissions;
using Municipal.Application.Legacy.Features.UserManagement.Permissions.Commands.CreatePermission;
using Municipal.Application.Legacy.Features.UserManagement.Permissions.Queries.GetAllPermissions;
using Municipal.Application.Legacy.Features.UserManagement.Permissions.Queries.GetPermissionsByRoleId;

namespace Municipal.Application.Legacy.Abstracts;

public interface IPermissionsRepository
{
    Task<Result<List<GetPermissionsResponse>>> GetAllPermissions(GetAllPermissionsRequest request, CancellationToken cancellationToken);
    Task<Result<GetPermissionsResponse>> GetAllPermissionsByRoleId(GetAllPermissionsByRoleIdRequest request, CancellationToken cancellationToken);
    Task<Result> DeleteUserPermissions(Guid userId, CancellationToken cancellationToken);
    Task<Result<string>> CreatePermission(CreatePermissionRequest request, CancellationToken cancellationToken);
}