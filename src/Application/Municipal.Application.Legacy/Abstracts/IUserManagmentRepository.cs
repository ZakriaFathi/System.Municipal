using FluentResults;
using Municipal.Application.Legacy.Features.UserManagement.Permissions;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.ChangePassword;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.ChangeUserActivation;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.CreateUser;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.CreateUserPermissions;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUser;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUserKyc;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUserPermissions;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUserProfile;
using Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUserKyc;
using Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUserProfile;
using Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUserRoles;
using Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUsers;
using Municipal.Utils.Vm;

namespace Municipal.Application.Legacy.Abstracts;

public interface IUserManagmentRepository
{
    Task<Result<string>> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken);
    Task<Result<string>> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken);
    Task<Result<string>> ChangeUserActivationAsync(ChangeUserActivationRequest request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateUserAsync(UpdateUserRequest request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateUserProfileAsync(UpdateUserProfileRequest request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateUserKycAsync(UpdateUserKycRequest request, CancellationToken cancellationToken);
    Task<Result<string>> CreateUserPermissionsAsync(CreateUserPermissionsRequest request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateUserPermissionsAsync(UpdateUserPermissionsRequest request, CancellationToken cancellationToken);
    Task<Result<GetUserProfileResponse>> GetUserProfileAsync(GetUserProfileRequset request, CancellationToken cancellationToken);
    Task<Result<GetUserKycResponse>> GetUserKycAsync(GetUserKycRequset request, CancellationToken cancellationToken);
    Task<Result<List<GetPermissionsResponse>>> GetUserRolesAndPermissionsAsync(GetUserRolesAndPermissionsRequest request, CancellationToken cancellationToken);
    Task<Result<PageResult<GetUsersResponse>>> GetUsersAsync(GetUsersRequest request, CancellationToken cancellationToken);
}