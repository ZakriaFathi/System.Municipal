using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Roles.Queries.GetAllRoles;

public class GetAllRolesRequestHandler : EndpointBase.WithRequestWithResult<GetAllRolesRequest, OperationResult<List<GetRolesResponse>>>
{
    private readonly IRoleRepository _roleService;

    public GetAllRolesRequestHandler(IRoleRepository roleService)
    {
        _roleService = roleService;
    }


    [HttpGet("api/UserManagement/GetAllRoles")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/GetAllRoles",
        Tags = new[] { "Roles" }
    )]
    public override async Task<OperationResult<List<GetRolesResponse>>> HandleAsync([FromQuery] GetAllRolesRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _roleService.GetAllRoles(request, cancellationToken);
        return responce.ToOperationResult();
    }
}