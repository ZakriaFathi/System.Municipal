using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Identity.Auth.Commands.SingUp;

public class SingUpRequestHandler : EndpointBase.WithRequestWithResult<SingUpCommand, OperationResult<string>>
{
    private readonly IAuthRepository _authService;

    public SingUpRequestHandler(IAuthRepository authService)
    {
        _authService = authService;
    }
    [HttpPost("api/Auth/SingUp")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Auth/SingUp",
        Tags = new[] { "Auth" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] SingUpCommand command, CancellationToken cancellationToken = default)
    {
        var responce = await _authService.SingUp(command, cancellationToken);
        return responce.ToOperationResult();
    }
}