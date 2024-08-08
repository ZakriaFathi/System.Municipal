using FluentResults;
using Municipal.Application.Legacy.Features.UserManagement.Roles;
using Municipal.Application.Legacy.Features.UserManagement.Roles.Queries.GetAllRoles;
using Municipal.Application.Legacy.Features.UserManagement.Roles.Queries.GetRolesByUserId;

namespace Municipal.Application.Legacy.Abstracts;

public interface IRoleRepository
{
    Task<Result<List<GetRolesResponse>>> GetAllRoles(GetAllRolesRequest request, CancellationToken cancellationToken);
    Task<Result<List<GetRolesResponse>>> GetRolesByUserId(GetRolesByUserIdRequest request, CancellationToken cancellationToken);
}