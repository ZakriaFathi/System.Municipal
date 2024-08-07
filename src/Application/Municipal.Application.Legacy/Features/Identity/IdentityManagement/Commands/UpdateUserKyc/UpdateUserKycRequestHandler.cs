using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.UpdateUserKyc;

public class UpdateUserKycRequestHandler : EndpointBase.WithRequestWithResult<UpdateUserKycRequest, OperationResult<string>>
{
    private readonly IIdentityManagementService _userManagementService;

    public UpdateUserKycRequestHandler(IIdentityManagementService userManagementService)
    {
        _userManagementService = userManagementService;
    }

    [HttpPost("api/IdentityManagement/UpdateUserKyc")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "IdentityManagement/UpdateUserKyc",
        Tags = new[] { "IdentityManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] UpdateUserKycRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagementService.UpdateUserKyc(request, cancellationToken);
        return responce.ToOperationResult();
    }
}