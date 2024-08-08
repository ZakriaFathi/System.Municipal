using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUser;

public class UpdateUserRequestHandler : EndpointBase.WithRequestWithResult<UpdateUserRequest, OperationResult<string>>
{
    private readonly IUserManagmentRepository _userManagment;

    public UpdateUserRequestHandler(IUserManagmentRepository userManagment)
    {
        _userManagment = userManagment;
    }

    [HttpPost("api/UserManagement/UpdateUser")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/UpdateUser",
        Tags = new[] { "UserManagement" }
    )]

    public override async Task<OperationResult<string>> HandleAsync([FromBody] UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagment.UpdateUserAsync(request, cancellationToken);
        return responce.ToOperationResult();
    }
}