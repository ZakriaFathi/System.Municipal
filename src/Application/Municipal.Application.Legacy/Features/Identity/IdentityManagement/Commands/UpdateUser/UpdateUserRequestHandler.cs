using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.UpdateUser;

public class UpdateUserRequestHandler : EndpointBase.WithRequestWithResult<UpdateUserRequest, OperationResult<string>>
{
    private readonly IIdentityManagementService _userManagementService;

    public UpdateUserRequestHandler(IIdentityManagementService userManagementService)
    {
        _userManagementService = userManagementService;
    }
    [HttpPost("api/IdentityManagement/UpdateUser")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "IdentityManagement/UpdateUser",
        Tags = new[] { "IdentityManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody]UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagementService.UpdateUser(request, cancellationToken);
        return responce.ToOperationResult();
    }
}