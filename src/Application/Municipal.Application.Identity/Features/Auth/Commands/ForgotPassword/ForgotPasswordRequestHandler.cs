using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Identity.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Identity.Features.Auth.Commands.ForgotPassword;

public class ForgotPasswordRequestHandler : EndpointBase.WithRequestWithResult<ForgotPasswordCommand, OperationResult<string>>
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
    public override async Task<OperationResult<string>> HandleAsync([FromBody] ForgotPasswordCommand command, CancellationToken cancellationToken = default)
    {
        var responce = await _authService.ForgotPassword(command, cancellationToken);
        return responce.ToOperationResult();
    }
}