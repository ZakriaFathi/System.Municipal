using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUserProfile;

public class UpdateUserProfileRequestHandler : EndpointBase.WithRequestWithResult<UpdateUserProfileRequest, OperationResult<string>>
{
    private readonly IUserManagmentRepository _userManagment;

    public UpdateUserProfileRequestHandler(IUserManagmentRepository userManagment)
    {
        _userManagment = userManagment;
    }

    [HttpPost("api/UserManagement/UpdateUserProfile")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/UpdateUserProfile",
        Tags = new[] { "UserManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] UpdateUserProfileRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagment.UpdateUserProfileAsync(request, cancellationToken);
        return responce.ToOperationResult();
    }
}