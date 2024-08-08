using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUserPermissions;

public class UpdateUserPermissionsRequestHandler : EndpointBase.WithRequestWithResult<UpdateUserPermissionsRequest, OperationResult<string>>
{
    private readonly IUserManagmentRepository _userManagment;

    public UpdateUserPermissionsRequestHandler(IUserManagmentRepository userManagment)
    {
        _userManagment = userManagment;
    }
    [HttpPost("api/UserManagement/UpdateUserPermissions")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/UpdateUserPermissions",
        Tags = new[] { "UserManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] UpdateUserPermissionsRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagment.UpdateUserPermissionsAsync(request, cancellationToken);
        return responce.ToOperationResult();
    }
}