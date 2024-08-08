using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Users.Commands.ChangePassword;

public class ChangePasswordRequestHandler : EndpointBase.WithRequestWithResult<ChangePasswordRequest, OperationResult<string>>
{
    private readonly IUserManagmentRepository _userManagment;

    public ChangePasswordRequestHandler(IUserManagmentRepository userManagment)
    {
        _userManagment = userManagment;
    }
    [HttpPost("api/UserManagement/ChangePassword")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/ChangePassword",
        Tags = new[] { "UserManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] ChangePasswordRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagment.ChangePasswordAsync(request, cancellationToken);
        return responce.ToOperationResult();
    }
}