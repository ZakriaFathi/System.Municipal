using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.UpdateUserProfile;

public class UpdateUserProfileRequestHandler : EndpointBase.WithRequestWithResult<UpdateUserProfileRequest, OperationResult<string>>
{
    private readonly IIdentityManagementService _userManagementService;
    public UpdateUserProfileRequestHandler(IIdentityManagementService userManagementService)
    {
        _userManagementService = userManagementService;
    }

    [HttpPost("api/IdentityManagement/UpdateUserProfile")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "IdentityManagement/UpdateUserProfile",
        Tags = new[] { "IdentityManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] UpdateUserProfileRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagementService.UpdateUserProfile(request, cancellationToken);
        return responce.ToOperationResult();
    }
}