using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.CreateUser;

public class CreateUserRequestHandler : EndpointBase.WithRequestWithResult<CreateUserRequest, OperationResult<string>>
{
    private readonly IIdentityManagementService _userManagementService;

    public CreateUserRequestHandler(IIdentityManagementService userManagementService)
    {
        _userManagementService = userManagementService;
    }

    [HttpPost("api/IdentityManagement/CreateUser")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "IdentityManagement/CreateUser",
        Tags = new[] { "IdentityManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagementService.CreateUser(request, cancellationToken);
        return responce.ToOperationResult();
    }
}