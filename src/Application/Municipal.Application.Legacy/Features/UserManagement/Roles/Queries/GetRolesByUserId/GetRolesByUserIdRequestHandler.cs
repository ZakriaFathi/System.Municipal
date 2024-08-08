using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Roles.Queries.GetRolesByUserId;

public class GetRolesByUserIdRequestHandler : EndpointBase.WithRequestWithResult<GetRolesByUserIdRequest, OperationResult<List<GetRolesResponse>>>
{
    private readonly IRoleRepository _roleService;

    public GetRolesByUserIdRequestHandler(IRoleRepository roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("api/UserManagement/GetRolesByUserId")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/GetRolesByUserId",
        Tags = new[] { "Roles" }
    )]
    public override async Task<OperationResult<List<GetRolesResponse>>> HandleAsync([FromQuery] GetRolesByUserIdRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _roleService.GetRolesByUserId(request, cancellationToken);
        return responce.ToOperationResult();
    }
}