using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Users.Commands.CreateUserPermissions;

public class CreateUserPermissionsRequestHandler : EndpointBase.WithRequestWithResult<CreateUserPermissionsRequest, OperationResult<string>>
{
    private readonly IUserManagmentRepository _userManagment;

    public CreateUserPermissionsRequestHandler(IUserManagmentRepository userManagementService)
    {
        _userManagment = userManagementService;
    }

    [HttpPost("api/UserManagement/CreateUserPermissions")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/CreateUserPermissions",
        Tags = new[] { "UserManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync(CreateUserPermissionsRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagment.CreateUserPermissionsAsync(request, cancellationToken);
        return responce.ToOperationResult();
    }
}