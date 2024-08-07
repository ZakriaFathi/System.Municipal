using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Identity.Auth.Commands.ResetPassword;

public class ResetPasswordRequestHandler : EndpointBase.WithRequestWithResult<ResetPasswordRequest, OperationResult<string>>
{
    private readonly IAuthRepository _authService;

    public ResetPasswordRequestHandler(IAuthRepository authService)
    {
        _authService = authService;
    }
    [HttpPost("api/Auth/ResetPassword")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Auth/ResetPassword",
        Tags = new[] { "Auth" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] ResetPasswordRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _authService.ResetPassword(request, cancellationToken);
        return responce.ToOperationResult();
    }
}