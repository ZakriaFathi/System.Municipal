using FluentResults;
using Municipal.Application.Identity.Features.Auth.Commands.ForgotPassword;
using Municipal.Application.Identity.Features.Auth.Commands.RefreshToken;
using Municipal.Application.Identity.Features.Auth.Commands.ResetPassword;
using Municipal.Application.Identity.Features.Auth.Commands.SingIn;
using Municipal.Application.Identity.Features.Auth.Commands.SingInOtp;
using Municipal.Application.Identity.Features.Auth.Commands.SingUp;
using Municipal.Application.Identity.Features.Auth.Responses;


namespace Municipal.Application.Identity.Abstracts;

public interface IAuthRepository
{
    Task<Result<string>> SingUp(SingUpCommand command, CancellationToken cancellationToken);
    Task<Result<AccessTokenRsponse>> SingIn(SingInCommand command, CancellationToken cancellationToken);
    Task<Result<AccessTokenRsponse>> SingInOtp(SingInOtpCommand command, CancellationToken cancellationToken);
    Task<Result<RefreshTokenRsponse>> RefreshToken(RefreshTokenClientCommand command, CancellationToken cancellationToken);
    Task<Result<string>> ResetPassword(ResetPasswordCommand command, CancellationToken cancellationToken);
    Task<Result<string>> ForgotPassword(ForgotPasswordCommand command, CancellationToken cancellationToken);
}