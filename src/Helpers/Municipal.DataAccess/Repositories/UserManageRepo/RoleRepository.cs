using FluentResults;
using Microsoft.EntityFrameworkCore;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Application.Legacy.Features.UserManagement.Roles;
using Municipal.Application.Legacy.Features.UserManagement.Roles.Commands.CreateRole;
using Municipal.Application.Legacy.Features.UserManagement.Roles.Queries.GetAllRoles;
using Municipal.Application.Legacy.Features.UserManagement.Roles.Queries.GetRolesByUserId;
using Municipal.DataAccess.Persistence;
using Municipal.Domin.Entities;

namespace Municipal.DataAccess.Repositories.UserManageRepo;

public class RoleRepository : IRoleRepository
{
    private readonly UserManagementDbContext _userManagementDb;

    public RoleRepository(UserManagementDbContext userManagementDb)
    {
        _userManagementDb = userManagementDb;
    }

    public async Task<Result<List<GetRolesResponse>>> GetAllRoles(GetAllRolesRequest request, CancellationToken cancellationToken)
    {
        var roles = await _userManagementDb.Roles.Select(x => new GetRolesResponse()
        {
            Id = x.Id,
            Name = x.Name
        }).ToListAsync(cancellationToken: cancellationToken);

        return roles;
    }

    public async Task<Result<List<GetRolesResponse>>> GetRolesByUserId(GetRolesByUserIdRequest request, CancellationToken cancellationToken)
    {
        var roles = await _userManagementDb.UserPermissions
            .Include(up => up.Permission)
            .ThenInclude(p => p.Role)
            .Where(up => up.UserId == request.UserId)
            .Select(x => new GetRolesResponse()
            {
                Id = x.Permission.Role.Id,
                Name = x.Permission.Role.Name
            }).Distinct().ToListAsync( cancellationToken);

        return roles;
    }

    public async Task<Result<string>> CreateRole(CreateRoleRequest request, CancellationToken cancellationToken)
    {
        var role = await _userManagementDb.Roles.FirstOrDefaultAsync(x=>x.Name == request.RoleName, cancellationToken);
        if (role != null)
            return Result.Fail("Role already exists");
        
        role = new Role
        {
            Name = request.RoleName
        };
        _userManagementDb.Roles.Add(role);
        await _userManagementDb.SaveChangesAsync(cancellationToken);
        return "تم الاضافة";
    }
}