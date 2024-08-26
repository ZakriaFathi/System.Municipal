using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Permissions.Commands.CreatePermission;

public class CreatePermissionRequestHandler : EndpointBase.WithRequestWithResult<CreatePermissionRequest, OperationResult<string>>
{
    private readonly IPermissionsRepository _permissionsRepository;
    public CreatePermissionRequestHandler(IPermissionsRepository permissionsRepository)
    {
        _permissionsRepository = permissionsRepository;
    }
    [HttpPost("api/UserManagement/CreatePermission")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/CreatePermission",
        Tags = new[] { "Permissions" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody]CreatePermissionRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _permissionsRepository.CreatePermission(request, cancellationToken);
        return responce.ToOperationResult();    }
}