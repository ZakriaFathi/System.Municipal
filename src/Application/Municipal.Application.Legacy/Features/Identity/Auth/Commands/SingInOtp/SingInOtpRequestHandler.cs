using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Application.Legacy.Features.Identity.Auth.Responses;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Identity.Auth.Commands.SingInOtp;

public class SingInOtpRequestHandler : EndpointBase.WithRequestWithResult<SingInOtpRequest, OperationResult<AccessTokenRsponse>>
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
    public override async Task<OperationResult<AccessTokenRsponse>> HandleAsync([FromQuery] SingInOtpRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _authService.SingInOtp(request, cancellationToken);
        return responce.ToOperationResult();
    }
}