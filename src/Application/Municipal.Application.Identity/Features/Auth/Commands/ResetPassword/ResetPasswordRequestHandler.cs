using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Identity.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Identity.Features.Auth.Commands.ResetPassword;

public class ResetPasswordRequestHandler : EndpointBase.WithRequestWithResult<ResetPasswordCommand, OperationResult<string>>
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
    public override async Task<OperationResult<string>> HandleAsync([FromBody] ResetPasswordCommand command, CancellationToken cancellationToken = default)
    {
        var responce = await _authService.ResetPassword(command, cancellationToken);
        return responce.ToOperationResult();
    }
}