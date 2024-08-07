using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Application.Legacy.Features.Identity.Auth.Responses;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Identity.Auth.Commands.SingIn;

public class SingInRequestHandler : EndpointBase.WithRequestWithResult<SingInRequest, OperationResult<AccessTokenRsponse>>
{
    private readonly IAuthRepository _authService;

    public SingInRequestHandler(IAuthRepository authService)
    {
        _authService = authService;
    }
    [HttpPost("api/Auth/SingInUserName")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Auth/SingInUserName",
        Tags = new[] { "Auth" }
    )]
    public override async Task<OperationResult<AccessTokenRsponse>> HandleAsync([FromQuery] SingInRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _authService.SingIn(request, cancellationToken);
        return responce.ToOperationResult();
    }
}