using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Identity.Auth.Commands.ForgotPassword;

public class ForgotPasswordRequestHandler : EndpointBase.WithRequestWithResult<ForgotPasswordRequest, OperationResult<string>>
{
    private readonly IAuthRepository _authService;

    public ForgotPasswordRequestHandler(IAuthRepository authService)
    {
        _authService = authService;
    }
    [HttpPost("api/Auth/ForgotPassword")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Auth/ForgotPassword",
        Tags = new[] { "Auth" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] ForgotPasswordRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _authService.ForgotPassword(request, cancellationToken);
        return responce.ToOperationResult();
    }
}