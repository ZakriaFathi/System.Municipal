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
    Task<Result<string>> SingUp(SingUpCommand command, CancellationToken cancellationToken);
    Task<Result<AccessTokenRsponse>> SingIn(SingInCommand command, CancellationToken cancellationToken);
    Task<Result<AccessTokenRsponse>> SingInOtp(SingInOtpCommand command, CancellationToken cancellationToken);
    Task<Result<RefreshTokenRsponse>> RefreshToken(RefreshTokenClientCommand command, CancellationToken cancellationToken);
    Task<Result<string>> ResetPassword(ResetPasswordCommand command, CancellationToken cancellationToken);
    Task<Result<string>> ForgotPassword(ForgotPasswordCommand command, CancellationToken cancellationToken);
}