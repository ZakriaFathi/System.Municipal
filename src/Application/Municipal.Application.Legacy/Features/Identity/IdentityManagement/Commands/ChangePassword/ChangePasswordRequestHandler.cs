using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.ChangePassword;

public class ChangePasswordRequestHandler : EndpointBase.WithRequestWithResult<ChangePasswordRequest, OperationResult<string>>
{
    private readonly IIdentityManagementService _userManagementService;
    public ChangePasswordRequestHandler(IIdentityManagementService userManagementService)
    {
        _userManagementService = userManagementService;
    }


    [HttpPost("api/IdentityManagement/ChangePassword")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "IdentityManagement/ChangePassword",
        Tags = new[] { "IdentityManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody]ChangePasswordRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagementService.ChangePassword(request, cancellationToken);
        return responce.ToOperationResult();
    }
}