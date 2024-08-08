using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Application.Legacy.Features.UserManagement.Permissions;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUserRoles;

public class GetUserRolesAndPermissionsRequestHandler : EndpointBase.WithRequestWithResult<GetUserRolesAndPermissionsRequest, OperationResult<List<GetPermissionsResponse>>>
{
    private readonly IUserManagmentRepository _userManagment;

    public GetUserRolesAndPermissionsRequestHandler(IUserManagmentRepository userManagment)
    {
        _userManagment = userManagment;
    }

    [HttpGet("api/UserManagement/GetUserRolesAndPermissions")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/GetUserRolesAndPermissions",
        Tags = new[] { "UserManagement" }
    )]
    public override async Task<OperationResult<List<GetPermissionsResponse>>> HandleAsync([FromQuery] GetUserRolesAndPermissionsRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagment.GetUserRolesAndPermissionsAsync(request, cancellationToken);
        return responce.ToOperationResult();
    }
}