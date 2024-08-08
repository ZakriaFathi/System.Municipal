using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Permissions.Queries.GetAllPermissions;

public class GetAllPermissionsRequestHandler : EndpointBase.WithRequestWithResult<GetAllPermissionsRequest, OperationResult<List<GetPermissionsResponse>>>
{
    private readonly IPermissionsRepository _permissionsService;

    public GetAllPermissionsRequestHandler(IPermissionsRepository permissionsService)
    {
        _permissionsService = permissionsService;
    }

    [HttpGet("api/UserManagement/GetAllPermissions")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/GetAllPermissions",
        Tags = new[] { "Permissions" }
    )]
    public override async Task<OperationResult<List<GetPermissionsResponse>>> HandleAsync([FromQuery] GetAllPermissionsRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _permissionsService.GetAllPermissions(request, cancellationToken);
        return responce.ToOperationResult();
    }
}