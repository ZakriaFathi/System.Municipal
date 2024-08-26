using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Roles.Commands.CreateRole;

public class CreateRoleRequestHandler : EndpointBase.WithRequestWithResult<CreateRoleRequest, OperationResult<string>>
{
    private readonly IRoleRepository _roleRepository;

    public CreateRoleRequestHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpPost("api/UserManagement/CreateRole")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/CreateRole",
        Tags = new[] { "Roles" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody]CreateRoleRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _roleRepository.CreateRole(request, cancellationToken);
        return responce.ToOperationResult(); 
    }
}