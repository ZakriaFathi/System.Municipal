using FluentResults;
using Municipal.Application.Identity.Features.IdentityManagement.Commands.CreateUser;
using Municipal.Application.Identity.Features.IdentityManagement.Commands.ChangePassword;
using Municipal.Application.Identity.Features.IdentityManagement.Commands.ChangeUserActivation;
using Municipal.Application.Identity.Features.IdentityManagement.Commands.CreateUserClimas;
using Municipal.Application.Identity.Features.IdentityManagement.Commands.UpdateUser;
using Municipal.Application.Identity.Features.IdentityManagement.Commands.UpdateUserProfile;
using Municipal.Application.Identity.Features.IdentityManagement.Commands.UpdateUserKyc;
using Municipal.Application.Identity.Features.IdentityManagement.Commands.UpdateUserClaims;


namespace Municipal.Application.Identity.Abstracts;

public interface IIdentityManagementRepository
{
    Task<Result<string>> CreateUser(CreateUserCommand command, CancellationToken cancellationToken);
    Task<Result<string>> ChangePassword(ChangePasswordCommand command, CancellationToken cancellationToken);
    Task<Result<string>> ChangeUserActivation(ChangeUserActivationCommand command, CancellationToken cancellationToken);
    Task<Result<string>> UpdateUser(UpdateUserCommand command, CancellationToken cancellationToken);
    Task<Result<string>> UpdateUserProfile(UpdateUserProfileCommand command, CancellationToken cancellationToken);
    Task<Result<string>> UpdateUserKyc(UpdateUserKycCommand command, CancellationToken cancellationToken);
    Task<Result<string>> CreateUserClaims(CreateUserClaimsCommand command, CancellationToken cancellationToken);
    Task<Result<string>> UpdateUserClaims(UpdateUserClaimsCommand command, CancellationToken cancellationToken);
}