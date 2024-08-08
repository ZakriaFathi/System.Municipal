using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Identity.Abstracts;
using Municipal.Application.Identity.Features.Auth.Responses;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Identity.Features.Auth.Commands.RefreshToken;

public class RefreshTokenRequestHandler : EndpointBase.WithRequestWithResult<RefreshTokenClientCommand, OperationResult<RefreshTokenRsponse>>
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
    public override async Task<OperationResult<RefreshTokenRsponse>> HandleAsync([FromQuery] RefreshTokenClientCommand command, CancellationToken cancellationToken = default)
    {
        var responce = await _authService.RefreshToken(command, cancellationToken);
        return responce.ToOperationResult();
    }
}