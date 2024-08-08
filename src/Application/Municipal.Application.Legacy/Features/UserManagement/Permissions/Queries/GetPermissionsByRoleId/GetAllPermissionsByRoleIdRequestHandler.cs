using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Permissions.Queries.GetPermissionsByRoleId;

public class GetAllPermissionsByRoleIdRequestHandler : EndpointBase.WithRequestWithResult<GetAllPermissionsByRoleIdRequest, OperationResult<GetPermissionsResponse>>
{
    private readonly IPermissionsRepository _permissionsService;

    public GetAllPermissionsByRoleIdRequestHandler(IPermissionsRepository permissionsService)
    {
        _permissionsService = permissionsService;
    }

    [HttpGet("api/UserManagement/GetAllPermissionsByRoleId")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/GetAllPermissionsByRoleId",
        Tags = new[] { "Permissions" }
    )]

    public override async Task<OperationResult<GetPermissionsResponse>> HandleAsync([FromQuery] GetAllPermissionsByRoleIdRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _permissionsService.GetAllPermissionsByRoleId(request, cancellationToken);
        return responce.ToOperationResult();
    }
}