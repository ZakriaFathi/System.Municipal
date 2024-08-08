using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Identity.Abstracts;
using Municipal.Application.Identity.Features.Auth.Responses;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Identity.Features.Auth.Commands.SingInOtp;

public class SingInOtpRequestHandler : EndpointBase.WithRequestWithResult<SingInOtpCommand, OperationResult<AccessTokenRsponse>>
{
    private readonly IAuthRepository _authService;

    public SingInOtpRequestHandler(IAuthRepository authService)
    {
        _authService = authService;
    }
    [HttpPost("api/Auth/SingInOtp")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Auth/SingInOtp",
        Tags = new[] { "Auth" }
    )]
    public override async Task<OperationResult<AccessTokenRsponse>> HandleAsync([FromQuery] SingInOtpCommand command, CancellationToken cancellationToken = default)
    {
        var responce = await _authService.SingInOtp(command, cancellationToken);
        return responce.ToOperationResult();
    }
}