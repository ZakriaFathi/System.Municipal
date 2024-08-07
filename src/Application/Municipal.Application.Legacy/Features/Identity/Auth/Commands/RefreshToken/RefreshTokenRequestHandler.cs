using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Application.Legacy.Features.Identity.Auth.Responses;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Identity.Auth.Commands.RefreshToken;

public class RefreshTokenRequestHandler : EndpointBase.WithRequestWithResult<RefreshTokenClientRequest, OperationResult<RefreshTokenRsponse>>
{
    private readonly IAuthRepository _authService;

    public RefreshTokenRequestHandler(IAuthRepository authService)
    {
        _authService = authService;
    }
    [HttpPost("api/Auth/RefreshToken")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Auth/RefreshToken",
        Tags = new[] { "Auth" }
    )]
    public override async Task<OperationResult<RefreshTokenRsponse>> HandleAsync([FromQuery] RefreshTokenClientRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _authService.RefreshToken(request, cancellationToken);
        return responce.ToOperationResult();
    }
}