using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.UpdateUserClaims;

public class UpdateUserClaimsRequestHandler : EndpointBase.WithRequestWithResult<UpdateUserClaimsRequest, OperationResult<string>>
{
    private readonly IIdentityManagementService _userManagementService;

    public UpdateUserClaimsRequestHandler(IIdentityManagementService userManagementService)
    {
        _userManagementService = userManagementService;
    }

    [HttpPost("api/IdentityManagement/UpdateUserClaims")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "IdentityManagement/UpdateUserClaims",
        Tags = new[] { "IdentityManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] UpdateUserClaimsRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagementService.UpdateUserClaims(request, cancellationToken);
        return responce.ToOperationResult();
    }
}