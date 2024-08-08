using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Users.Commands.CreateUser;

public class CreateUserRequestHandler : EndpointBase.WithRequestWithResult<CreateUserRequest, OperationResult<string>>
{
    private readonly IUserManagmentRepository _userManagment;

    public CreateUserRequestHandler(IUserManagmentRepository userManagment)
    {
        _userManagment = userManagment;
    }

    [HttpPost("api/UserManagement/CreateUser")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/CreateUser",
        Tags = new[] { "UserManagement" }
    )]

    public override async Task<OperationResult<string>> HandleAsync([FromBody] CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagment.CreateUserAsync(request, cancellationToken);
        return responce.ToOperationResult();
    }
}