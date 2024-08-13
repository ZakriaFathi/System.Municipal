using FluentResults;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.ChangePassword;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.ChangeUserActivation;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.CreateUser;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.CreateUserPermissions;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUser;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUserKyc;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUserPermissions;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUserProfile;
using Municipal.Application.Legacy.Models.IdentityModel;
using Municipal.Application.Legacy.Models.UserManagement;
using Municipal.Domin.Entities;
using Municipal.Domin.Models;

namespace Municipal.Application.Legacy.Abstracts;

public interface ISherdUserRepository
{
    #region Identity
    Task<Result<AppUser>> GetIdentityUserById(string userId, CancellationToken cancellationToken);
    Task<Result<AppUser>> GetIdentityUserByUserName(string userName, CancellationToken cancellationToken);
    Task<Result<string>> InsertIdentityUser(InsertAndUpdateIdentityUser command, CancellationToken cancellationToken);
    Task<Result<string>> UpdateIdentityUser(InsertAndUpdateIdentityUser command, CancellationToken cancellationToken);
    Task<Result<string>> UpdateIdentityUserProfile(UpdateIdentityProfile command, CancellationToken cancellationToken);
    Task<Result<string>> UpdateIdentityUserKyc(UpdateIdentityKyc command, CancellationToken cancellationToken);
    Task<Result<string>> ChangeIdentityPassword(ChangeIdentityPassword command, CancellationToken cancellationToken);
    Task<Result<string>> ChangeIdentityActivation(ChangeIdentityActivation command, CancellationToken cancellationToken);
    Task<Result<string>> InsertIdentityUserClaims(InsertAndUpdateIdentityClaims command, CancellationToken cancellationToken);
    Task<Result<string>> UpdateIdentityUserClaims(InsertAndUpdateIdentityClaims command, CancellationToken cancellationToken);
   

    #endregion

    #region UserManagement

    Task<Result<User>> GetUserById(Guid userId, CancellationToken cancellationToken);
    Task<Result<User>> GetUserByUserName(string userName, CancellationToken cancellationToken);
    Task<Result<UserKyc>> GetUserKycById(Guid userId, CancellationToken cancellationToken);
    Task<Result<string>> InsertUser(InsertAndUpdateUserProfile request, CancellationToken cancellationToken);
    Task<Result<string>> InsertUserKyc(InsertAndUpdateUserKyc request, CancellationToken cancellationToken);
    Task<Result<string>> ChangePassword(ChangePassword request, CancellationToken cancellationToken);
    Task<Result<string>> ChangeUserActivation(ChangeUserActivation request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateUserProfile(InsertAndUpdateUserProfile request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateUserKyc(InsertAndUpdateUserKyc request, CancellationToken cancellationToken);
    Task<Result<string>> CreateUserPermissions(InsertAndUpdateUserPermissions request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateUserPermissions(InsertAndUpdateUserPermissions request, CancellationToken cancellationToken);

    #endregion

}