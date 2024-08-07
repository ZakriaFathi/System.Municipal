using FluentResults;
using Municipal.Application.Legacy.Features.Identity.Auth.Commands.ForgotPassword;
using Municipal.Application.Legacy.Features.Identity.Auth.Commands.RefreshToken;
using Municipal.Application.Legacy.Features.Identity.Auth.Commands.ResetPassword;
using Municipal.Application.Legacy.Features.Identity.Auth.Commands.SingIn;
using Municipal.Application.Legacy.Features.Identity.Auth.Commands.SingInOtp;
using Municipal.Application.Legacy.Features.Identity.Auth.Commands.SingUp;
using Municipal.Application.Legacy.Features.Identity.Auth.Responses;

namespace Municipal.Application.Legacy.Abstracts;

public interface IAuthRepository
{
    Task<Result<string>> SingUp(SingUpRequest request, CancellationToken cancellationToken);
    Task<Result<AccessTokenRsponse>> SingIn(SingInRequest request, CancellationToken cancellationToken);
    Task<Result<AccessTokenRsponse>> SingInOtp(SingInOtpRequest request, CancellationToken cancellationToken);
    Task<Result<RefreshTokenRsponse>> RefreshToken(RefreshTokenClientRequest request, CancellationToken cancellationToken);
    Task<Result<string>> ResetPassword(ResetPasswordRequest request, CancellationToken cancellationToken);
    Task<Result<string>> ForgotPassword(ForgotPasswordRequest request, CancellationToken cancellationToken);
}