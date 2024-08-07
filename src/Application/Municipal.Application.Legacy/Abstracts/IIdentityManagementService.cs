using FluentResults;
using Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.ChangePassword;
using Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.ChangeUserActivation;
using Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.CreateUser;
using Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.CreateUserClimas;
using Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.UpdateUser;
using Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.UpdateUserClaims;
using Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.UpdateUserKyc;
using Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.UpdateUserProfile;

namespace Municipal.Application.Legacy.Abstracts;

public interface IIdentityManagementService
{
    Task<Result<string>> CreateUser(CreateUserRequest request, CancellationToken cancellationToken);
    Task<Result<string>> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken);
    Task<Result<string>> ChangeUserActivation(ChangeUserActivationRequest request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateUser(UpdateUserRequest request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateUserProfile(UpdateUserProfileRequest request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateUserKyc(UpdateUserKycRequest request, CancellationToken cancellationToken);
    Task<Result<string>> CreateUserClaims(CreateUserClaimsRequest request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateUserClaims(UpdateUserClaimsRequest request, CancellationToken cancellationToken);
}