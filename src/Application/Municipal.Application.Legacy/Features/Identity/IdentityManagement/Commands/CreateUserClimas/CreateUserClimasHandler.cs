using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.CreateUserClimas;

public class CreateUserClimasHandler : EndpointBase.WithRequestWithResult<CreateUserClaimsRequest, OperationResult<string>>
{
    private readonly IIdentityManagementService _userManagementService;

    public CreateUserClimasHandler(IIdentityManagementService userManagementService)
    {
        _userManagementService = userManagementService;
    }

    [HttpPost("api/IdentityManagement/CreateUserClaims")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "IdentityManagement/CreateUserClaims",
        Tags = new[] { "IdentityManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] CreateUserClaimsRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagementService.CreateUserClaims(request, cancellationToken);
        return responce.ToOperationResult();
    }
}