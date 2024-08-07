using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.ChangeUserActivation;

public class ChangeUserActivationRequestHandler : EndpointBase.WithRequestWithResult<ChangeUserActivationRequest, OperationResult<string>>
{
    private readonly IIdentityManagementService _userManagementService;
    public ChangeUserActivationRequestHandler(IIdentityManagementService userManagementService)
    {
        _userManagementService = userManagementService;
    }

    [HttpPost("api/IdentityManagement/ChangeUserActivation")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "IdentityManagement/ChangeUserActivation",
        Tags = new[] { "IdentityManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody]ChangeUserActivationRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagementService.ChangeUserActivation(request, cancellationToken);
        return responce.ToOperationResult(); 
    }
}